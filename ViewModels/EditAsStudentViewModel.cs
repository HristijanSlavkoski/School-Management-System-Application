using School_Management_System_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System_Application.ViewModels
{
    public class EditAsStudentViewModel
    {
        public Enrollment enrollment { get; set; }

        [Display(Name = "Seminal File")]
        public IFormFile? seminalUrlFile { get; set; }

        public string? seminalUrlName { get; set; }
    }
}
