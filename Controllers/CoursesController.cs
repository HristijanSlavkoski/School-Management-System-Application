#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Data;
using School_Management_System_Application.Models;
using School_Management_System_Application.ViewModels;

namespace School_Management_System_Application.Controllers
{
    public class CoursesController : Controller
    {
        private readonly School_Management_System_ApplicationContext _context;

        public CoursesController(School_Management_System_ApplicationContext context)
        {
            _context = context;
        }

        // GET: Courses
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string title, int semester, string programme)
        {
            IQueryable<Course> coursesQuery = _context.Course.AsQueryable();
            IQueryable<int> semestersQuery = _context.Course.OrderBy(m => m.semester).Select(m => m.semester).Distinct();
            IQueryable<string> programmesQuery = _context.Course.OrderBy(m => m.programme).Select(m => m.programme).Distinct();
            if (!string.IsNullOrEmpty(title))
            {
                coursesQuery = coursesQuery.Where(x => x.title.Contains(title));
            }
            if (semester != null && semester != 0)
            {
                coursesQuery = coursesQuery.Where(s => s.semester == semester);
            }
            if (!string.IsNullOrEmpty(programme))
            {
                coursesQuery = coursesQuery.Where(p => p.programme == programme);
            }
            var CoursefilterVM = new CourseFilter
            {
                courses = await coursesQuery.Include(c => c.firstTeacher).Include(c => c.secondTeacher).ToListAsync(),
                programmes = new SelectList(await programmesQuery.ToListAsync()),
                semesters = new SelectList(await semestersQuery.ToListAsync())
            };

            return View(CoursefilterVM);
        }

        // GET: Courses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.firstTeacher)
                .Include(c => c.secondTeacher)
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(_context.Set<Teacher>(), "teacherId", "fullName");
            ViewData["Students"] = new SelectList(_context.Set<Student>(), "Id", "fullName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("courseId,title,credits,semester,programme,educationLevel,firstTeacherId,firstTeacher,secondTeacherId,secondTeacher,enrollments")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Teachers"] = new SelectList(_context.Set<Teacher>(), "teacherId", "fullName");
            ViewData["Students"] = new SelectList(_context.Set<Student>(), "Id", "fullName");
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Course.Where(m => m.courseId == id).Include(x => x.enrollments).First();
            IQueryable<Course> coursesQuery = _context.Course.AsQueryable();
            coursesQuery = coursesQuery.Where(m => m.courseId == id);
            if (course == null)
            {
                return NotFound();
            }
            var students = _context.Student.AsEnumerable();
            students = students.OrderBy(s => s.fullName);

            EnrollStudentsAtCourseEdit viewmodel = new EnrollStudentsAtCourseEdit
            {
                course = await coursesQuery.Include(c => c.firstTeacher).Include(c => c.secondTeacher).FirstAsync(),
                studentsEnrolledList = new MultiSelectList(students, "Id", "fullName"),
                selectedStudents = course.enrollments.Select(sa => sa.studentId)
            };

            ViewData["Teachers"] = new SelectList(_context.Set<Teacher>(), "teacherId", "fullName");
            //ViewData["Students"] = new SelectList(_context.Set<Student>(), "Id", "fullName", course.enrollments);
            return View(viewmodel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, EnrollStudentsAtCourseEdit viewmodel)
        {
            if (id != viewmodel.course.courseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.course);
                    await _context.SaveChangesAsync();

                    var course = _context.Course.Where(m => m.courseId == id).First();
                    string semester;
                    if (course.semester % 2 == 0)
                    {
                        semester = "leten";
                    }
                    else
                    {
                        semester = "zimski";
                    }
                    IEnumerable<long> selectedStudents = viewmodel.selectedStudents;
                    if (selectedStudents != null)
                    {
                        IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !selectedStudents.Contains(s.studentId) && s.courseId == id);
                        _context.Enrollment.RemoveRange(toBeRemoved);

                        IEnumerable<long> existEnrollments = _context.Enrollment.Where(s => selectedStudents.Contains(s.studentId) && s.courseId == id).Select(s => s.studentId);
                        IEnumerable<long> newEnrollments = selectedStudents.Where(s => !existEnrollments.Contains(s));

                        foreach (int studentId in newEnrollments)
                            _context.Enrollment.Add(new Enrollment { studentId = studentId, courseId = id , semester = semester, year = viewmodel.year});

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => s.courseId == id);
                        _context.Enrollment.RemoveRange(toBeRemoved);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewmodel.course.courseId))
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
            return View(viewmodel);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.courseId == id);
        }


        // GET: Courses/CoursesTeaching/5
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> CoursesTeaching(int? id, string title, int semester, string programme)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.teacherId == id);
            ViewBag.Message = teacher.fullName;
            IQueryable<Course> coursesQuery = _context.Course.Where(m => m.firstTeacherId == id || m.secondTeacherId == id);
            await _context.SaveChangesAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            IQueryable<int> semestersQuery = _context.Course.OrderBy(m => m.semester).Select(m => m.semester).Distinct();
            IQueryable<string> programmesQuery = _context.Course.OrderBy(m => m.programme).Select(m => m.programme).Distinct();
            if (!string.IsNullOrEmpty(title))
            {
                coursesQuery = coursesQuery.Where(x => x.title.Contains(title));
            }
            if (semester != null && semester != 0)
            {
                coursesQuery = coursesQuery.Where(s => s.semester == semester);
            }
            if (!string.IsNullOrEmpty(programme))
            {
                coursesQuery = coursesQuery.Where(p => p.programme == programme);
            }
            var CourseFilterVM = new CourseFilter
            {
                courses = await coursesQuery.Include(c => c.firstTeacher).Include(c => c.secondTeacher).ToListAsync(),
                programmes = new SelectList(await programmesQuery.ToListAsync()),
                semesters = new SelectList(await semestersQuery.ToListAsync())
            };

            return View(CourseFilterVM);
        }
    }
}
