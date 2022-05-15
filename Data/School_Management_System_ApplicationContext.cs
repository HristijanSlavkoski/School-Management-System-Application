#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Areas.Identity.Data;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.Data
{
    public class School_Management_System_ApplicationContext : IdentityDbContext<User>
    {
        public School_Management_System_ApplicationContext (DbContextOptions<School_Management_System_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<School_Management_System_Application.Models.Student> Student { get; set; }

        public DbSet<School_Management_System_Application.Models.Teacher> Teacher { get; set; }

        public DbSet<School_Management_System_Application.Models.Course> Course { get; set; }

        public DbSet<School_Management_System_Application.Models.Enrollment> Enrollment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
