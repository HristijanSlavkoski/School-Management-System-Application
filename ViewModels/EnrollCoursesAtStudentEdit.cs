using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System_Application.ViewModels
{
    public class EnrollCoursesAtStudentEdit
    {
        public Student student { get; set; }

        public IEnumerable<int>? selectedCourses { get; set; }

        public IEnumerable<SelectListItem>? coursesEnrolledList { get; set; }

        [Display(Name = "Year")]
        public int? year { get; set; }

        [Display(Name = "Semester")]
        public string? semester { get; set; }

        public string? profilePictureName { get; set; }
    }
}
