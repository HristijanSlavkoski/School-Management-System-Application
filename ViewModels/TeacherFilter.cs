using Microsoft.AspNetCore.Mvc.Rendering;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.ViewModels
{
    public class TeacherFilter
    {
        public IList<Teacher> teachers { get; set; }

        public SelectList academicRanks { get; set; }

        public SelectList degrees { get; set; }

        public string fullName { get; set; }

        public string academicRank { get; set; }

        public string degree { get; set; }
    }
}
