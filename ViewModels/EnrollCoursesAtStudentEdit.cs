using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class EnrollCoursesAtStudentEdit
    {
        public Student student { get; set; }

        public IEnumerable<int>? selectedCourses { get; set; }

        public IEnumerable<SelectListItem>? coursesEnrolledList { get; set; }

        public int? year { get; set; }

        public string? semester { get; set; }
    }
}
