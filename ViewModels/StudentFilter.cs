using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class StudentFilter
    {
        public IList<Student> students { get; set; }

        public string fullName { get; set; }

        public string studentId { get; set; }
    }
}
