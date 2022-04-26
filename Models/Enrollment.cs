using System.ComponentModel.DataAnnotations;

namespace School_Management_System_Application.Models
{
    public class Enrollment
    {
        public long enrollmentId { get; set; }

        [Display(Name = "Course")]
        public int courseId { get; set; }
        [Display(Name = "Course")]
        public Course? course { get; set; }

        [Display(Name = "Student")]
        public long studentId { get; set; }
        [Display(Name = "Student")]
        public Student? student { get; set; }

        [Display(Name = "Semester")]
        public int? semester { get; set; }

        [Display(Name = "Year")]
        public int? year { get; set; }

        [Display(Name = "Grade")]
        public int? grade { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Seminal Url")]
        public string? seminalUrl { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [Display(Name = "Project Url")]
        public string? projectUrl { get; set; }

        [Display(Name = "Exam Points")]
        public int? examPoints { get; set; }

        [Display(Name = "Seminal Points")]
        public int? seminalPoints { get; set; }

        [Display(Name = "Project Points")]
        public int? projectPoints { get; set; }

        [Display(Name = "Additional Points")]
        public int? additionalPoints { get; set; }

        [Display(Name = "Finish Date")]
        [DataType(DataType.Date)]
        public DateTime? finishDate { get; set; }

        [Display(Name = "Total Points")]
        public int totalPoints
        {
            get
            {
                if (examPoints == null)
                    examPoints = 0;
                if (seminalPoints == null)
                    seminalPoints = 0;
                if (projectPoints == null)
                    projectPoints = 0;
                if (additionalPoints == null)
                    additionalPoints = 0;
                int points = (int)(examPoints + seminalPoints + projectPoints + additionalPoints);
                return (int)points;
            }
        }
    }
}
