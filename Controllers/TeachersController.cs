#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Data;
using School_Management_System_Application.Models;
using School_Management_System_Application.ViewModels;

namespace School_Management_System_Application.Controllers
{
    public class TeachersController : Controller
    {
        private readonly School_Management_System_ApplicationContext _context;

        public TeachersController(School_Management_System_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string fullName, string academicrank, string degree)
        {

            IQueryable<Teacher> teachersQuery = _context.Teacher.AsQueryable();
            IQueryable<string> academicRanksQuery = _context.Teacher.OrderBy(m => m.academicRank).Select(m => m.academicRank).Distinct();
            IQueryable<string> degreesQuery = _context.Teacher.OrderBy(m => m.degree).Select(m => m.degree).Distinct();
            if (!string.IsNullOrEmpty(fullName))
            {
                if (fullName.Contains(" "))
                {
                    string[] names = fullName.Split(" ");
                    teachersQuery = teachersQuery.Where(x => x.firstName.Contains(names[0]) || x.lastName.Contains(names[1]) ||
                    x.firstName.Contains(names[1]) || x.lastName.Contains(names[0]));
                }
                else
                {
                    teachersQuery = teachersQuery.Where(x => x.firstName.Contains(fullName) || x.lastName.Contains(fullName));
                }
            }
            if (!string.IsNullOrEmpty(academicrank))
            {
                teachersQuery = teachersQuery.Where(x => x.academicRank.Contains(academicrank));
            }
            if (!string.IsNullOrEmpty(degree))
            {
                teachersQuery = teachersQuery.Where(x => x.degree.Contains(degree));
            }
            var TeacherFilterVM = new TeacherFilter
            {
                teachers = await teachersQuery.ToListAsync(),
                academicRanks = new SelectList(await academicRanksQuery.ToListAsync()),
                degrees = new SelectList(await degreesQuery.ToListAsync())
            };

            return View(TeacherFilterVM);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.teacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            EditPictureTeacher viewmodel = new EditPictureTeacher
            {
                teacher = teacher,
                profilePictureName = teacher.profilePicture
            };

            return View(viewmodel);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("teacherId,firstName,lastName,degree,academicRank,officeNumber,hireDate")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("teacherId,firstName,lastName,degree,academicRank,officeNumber,hireDate,profilePicture")] Teacher teacher)
        {
            if (id != teacher.teacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.teacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.teacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.teacherId == id);
        }
        // GET: Teachers/EditPicture/5
        public async Task<IActionResult> EditPicture(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = _context.Teacher.Where(x => x.teacherId == id).First();
            if (teacher == null)
            {
                return NotFound();
            }

            EditPictureTeacher viewmodel = new EditPictureTeacher
            {
                teacher = teacher,
                profilePictureName = teacher.profilePicture
            };

            return View(viewmodel);
        }

        // POST: Teachers/EditPicture/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPicture(long id, EditPictureTeacher viewmodel)
        {
            if (id != viewmodel.teacher.teacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewmodel.profilePictureFile != null)
                    {
                        string uniqueFileName = UploadedFile(viewmodel);
                        viewmodel.teacher.profilePicture = uniqueFileName;
                    }
                    else
                    {
                        viewmodel.teacher.profilePicture = viewmodel.profilePictureName;
                    }

                    _context.Update(viewmodel.teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(viewmodel.teacher.teacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = viewmodel.teacher.teacherId });
            }
            return View(viewmodel);
        }
        private string UploadedFile(EditPictureTeacher viewmodel)
        {
            string uniqueFileName = null;

            if (viewmodel.profilePictureFile != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profilePictures");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewmodel.profilePictureFile.FileName);
                string fileNameWithPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    viewmodel.profilePictureFile.CopyTo(stream);
                }
            }
            return uniqueFileName;
        }
    }
}
