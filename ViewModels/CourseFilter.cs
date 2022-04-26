using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class CourseFilter
    {
        public IList<Course> courses { get; set; }
        
        public SelectList programmes { get; set; }

        public SelectList semesters { get; set; }

        public string programme { get; set; }
        
        public string title { get; set; }

        public int semester { get; set; }
    }
}
