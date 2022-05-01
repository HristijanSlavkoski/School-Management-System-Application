using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class EnrollmentFilter
    {
        public IList<Enrollment> enrollments { get; set; }

        public SelectList yearsList { get; set; }
        public int year { get; set; }
    }
}
