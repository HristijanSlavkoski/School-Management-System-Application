using School_Management_System_Application.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System_Application.Models
{
    public class Student
    {
        public long Id { get; set; }

        [StringLength(10, MinimumLength = 3)]
        [Required]
        [Display(Name = "Student Index Card")]
        public string studentId { get; set; }
        
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        
        [StringLength(50, MinimumLength = 3)]
        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        public DateTime? enrollmentDate { get; set; }

        [Display(Name = "Acquired Credits")]
        public int? acquiredCredits { get; set; }

        [Display(Name = "Current Semester")]
        public int? currentSemester { get; set; }

        [StringLength(25, MinimumLength = 3)]
        [Display(Name = "Education Level")]
        public string? educationLevel { get; set; }

        public ICollection<Enrollment>? enrollments { get; set; }

        public string? profilePicture { get; set; }

        public string fullName
        {
            get
            {
                return string.Format("{0} {1}", firstName, lastName);
            }
        }

        public string? userIdentityId { get; set; }

        public User? userIdentity { get; set; }
    }
}
