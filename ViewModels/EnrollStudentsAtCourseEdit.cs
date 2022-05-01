using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class EnrollStudentsAtCourseEdit
    {
        public Course course { get; set; }

        public IEnumerable<long>? selectedStudents { get; set; }
        
        public IEnumerable<SelectListItem>? studentsEnrolledList { get; set; }

        public int? year { get; set; }
    }
}
