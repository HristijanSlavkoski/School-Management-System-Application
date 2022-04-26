#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Data;
using School_Management_System_Application.ViewModels;

namespace School_Management_System_Application.Models
{
    public class StudentsController : Controller
    {
        private readonly School_Management_System_ApplicationContext _context;

        public StudentsController(School_Management_System_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string fullName, string studentId)
        {
            IQueryable<Student> studentsQuery = _context.Student.AsQueryable();
            if(!string.IsNullOrEmpty(fullName))
            {
                if (fullName.Contains(" "))
                {
                    string[] names = fullName.Split(" ");
                    studentsQuery = studentsQuery.Where(x => x.firstName.Contains(names[0]) || x.lastName.Contains(names[1]) ||
                    x.firstName.Contains(names[1]) || x.lastName.Contains(names[0]));
                }
                else
                {
                    studentsQuery = studentsQuery.Where(x => x.firstName.Contains(fullName) || x.lastName.Contains(fullName));
                }
            }
            if(!string.IsNullOrEmpty(studentId))
            {
                studentsQuery = studentsQuery.Where(x => x.studentId.Contains(studentId));
            }
            var StudentFilterVM = new StudentFilter
            {
                students = await studentsQuery.ToListAsync()
            };

            return View(StudentFilterVM);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Courses"] = new SelectList(_context.Set<Course>(), "courseId", "title");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,studentId,firstName,lastName,enrollmentDate,acquiredCredits,currentSemester,educationLevel,enrollments")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Courses"] = new SelectList(_context.Set<Course>(), "courseId", "title");
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,studentId,firstName,lastName,enrollmentDate,acquiredCredits,currentSemester,educationLevel,enrollments")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
       
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(long id)
        {
            return _context.Student.Any(e => e.Id == id);
        }

        // GET: Students/StudentsEnrolled/5
        public async Task<IActionResult> StudentsEnrolled(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.courseId == id);
            ViewBag.Message = course.title;
            IQueryable<Student> studentQuery = _context.Enrollment.Where(x => x.courseId == id).Select(x => x.student);
            await _context.SaveChangesAsync();
            if (course == null)
            {
                return NotFound();
            }
            var studentFilterVM = new StudentFilter
            {
                students = await studentQuery.ToListAsync(),
            };

            return View(studentFilterVM);
        }
    }
}

