using School_Management_System_Application.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System_Application.Models
{
    public class Course
    {
        private readonly School_Management_System_ApplicationContext _context;
        public int courseId { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }
        
        [Required]
        [Display(Name = "Credits")]
        public int credits { get; set; }
        
        [Required]
        [Display(Name = "Semester")]
        public int semester { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Programme")]
        public string? programme { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Education Level")]
        public string? educationLevel { get; set; }
        
        [Display(Name = "First Teacher")]
        public int? firstTeacherId { get; set; }
        public Teacher? firstTeacher { get; set; }

        [Display(Name = "Second Teacher")]
        public int? secondTeacherId { get; set; }
        public Teacher? secondTeacher { get; set; }

        public ICollection<Enrollment>? enrollments { get; set; }
    }
}
