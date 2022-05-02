using School_Management_System_Application.Models;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System_Application.ViewModels
{
    public class EditPictureStudent
    {
        public Student? student { get; set; }

        [Display(Name = "Upload picture")]
        public IFormFile? profilePictureFile { get; set; }

        [Display(Name = "Picture name")]
        public string? profilePictureName { get; set; }
    }
}
