using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class CreateCourse
    {
        public IList<Course> courses { get; set; }

        public IList<Student> studentsEnrolled { get; set; }
    }
}
