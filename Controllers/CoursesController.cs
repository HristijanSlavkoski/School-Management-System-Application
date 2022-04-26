#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(string title, int semester, string programme)
        {
            IQueryable<Course> coursesQuery = _context.Course.AsQueryable();
            IQueryable<int> semestersQuery = _context.Course.OrderBy(m => m.semester).Select(m => m.semester).Distinct();
            IQueryable<string> programmesQuery = _context.Course.OrderBy(m => m.programme).Select(m => m.programme).Distinct();
            if (!string.IsNullOrEmpty(title))
            {
                coursesQuery = coursesQuery.Where(x => x.title.Contains(title));
            }
            if (semester!=null && semester!=0)
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
        public IActionResult Create()
        {
            ViewData["Teachers"] = new SelectList(_context.Set<Teacher>(), "teacherId", "fullName");
            ViewData["Students"] = new SelectList(_context.Set<Student>(), "Id", "fullName");
            //ViewData["Students"] = new SelectList(_context.Set<Enrollment>(), "enrollmentId", "studentId");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("courses, students")] CreateCourse course)
        public async Task<IActionResult> Create([Bind("courseId,title,credits,semester,programme,educationLevel,firstTeacherId,firstTeacher,secondTeacherId,secondTeacher,enrollments")] Course course)
        {
            /*            _context.Add(course.courses);
                        await _context.SaveChangesAsync();
                        IQueryable<Course> coursesQuery = _context.Course.AsQueryable();*/
            /*            _context.Enrollment.AddRange(
                            new Enrollment
                            {
                                courseId = _context.Course.FirstOrDefault(d => d.title == course.title).courseId,
                                studentId = _context.Student.Single(d => d.firstName == "Dimitar" && d.lastName == "Dimitrijoski").Id,
                            }
                        );
            */





            if (ModelState.IsValid)
            {

                //Console.WriteLine(okkk[0].courseId);
                //string naaame = "HI";

               /* _context.Enrollment.AddRange(
                    new Enrollment
                    {
                        courseId = _context.Course.FirstOrDefault(d => d.title == course.title).courseId,
                        studentId = _context.Student.Single(d => d.firstName == "Dimitar" && d.lastName == "Dimitrijoski").Id,
                    }
                );*/
                _context.Add(course);
                await _context.SaveChangesAsync();
               /* ICollection<Enrollment> okk2k = course.enrollments;
                Console.WriteLine(okk2k);*/
                return RedirectToAction(nameof(Index));
            }


            /*            var CreateCourse = new CreateCourse
                        {
                            courses = await coursesQuery.Include(c => c.firstTeacher).Include(c => c.secondTeacher).ToListAsync(),
                            studentsEnrolled = course.studentsEnrolled
                        };

                        return View(CreateCourse);*/

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Teachers"] = new SelectList(_context.Set<Teacher>(), "teacherId", "fullName");
            ViewData["Students"] = new SelectList(_context.Set<Student>(), "Id", "fullName");
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("courseId,title,credits,semester,programme,educationLevel,firstTeacherId,firstTeacher,secondTeacherId,secondTeacher,enrollments")] Course course)
        {
            if (id != course.courseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.courseId))
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
            return View(course);
        }

        // GET: Courses/Delete/5
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
        public async Task<IActionResult> CoursesTeaching(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.teacherId == id);
            ViewBag.Message = teacher.fullName;
            /*var courses = await _context.Course
                .FirstOrDefaultAsync(m => m.firstTeacherId == id || m.secondTeacherId == id);*/
            IQueryable<Course> coursesQuery = _context.Course.Where(m => m.firstTeacherId == id || m.secondTeacherId == id);
            await _context.SaveChangesAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            var CourseTitleVM = new CourseFilter
            {
                courses = await coursesQuery.ToListAsync(),
            };
            
            return View(CourseTitleVM);
        }
    }
}
