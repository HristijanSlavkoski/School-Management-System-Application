#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.Data
{
    public class School_Management_System_ApplicationContext : DbContext
    {
        public School_Management_System_ApplicationContext (DbContextOptions<School_Management_System_ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<School_Management_System_Application.Models.Student> Student { get; set; }

        public DbSet<School_Management_System_Application.Models.Teacher> Teacher { get; set; }

        public DbSet<School_Management_System_Application.Models.Course> Course { get; set; }

        public DbSet<School_Management_System_Application.Models.Enrollment> Enrollment { get; set; }

 /*       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            *//*modelBuilder.Entity<Course>()
                .HasKey(b => b.firstTeacherId);

            modelBuilder.Entity<Course>()
                .HasKey(b => b.secondTeacherId);

            modelBuilder.Entity<Teacher>()
                .HasKey(b => b.teacherId);*/

            /*modelBuilder.Entity<Course>()*/
            /*                .ToTable("Course")
                     .HasKey(x => new { x.firstTeacherId, x.secondTeacherId })
                     .<Teacher>(x => x.)
                        .WithMany(x => x.Friendships)
                        .HasForeignKey(x => x.Person1Id); 
                        .WithMany() 
                        .HasForeignKey(x => x.firstTeacherId);*/


            /* modelBuilder.Entity<Course>()
             .HasOne(x => x.firstTeacher)
                 .WithMany(x => x.courses)
                 .HasForeignKey(x => x.firstTeacherId);*/


            /* modelBuilder.Entity<Teacher>()
                 .HasMany(c => c.coursesOne)
                 .WithOne(e => e.firstTeacher)
                 .OnDelete(DeleteBehavior.NoAction);

             modelBuilder.Entity<Teacher>()
                 .HasMany(c => c.coursesTwo)
                 .WithOne(e => e.secondTeacher)
                 .OnDelete(DeleteBehavior.NoAction);

             modelBuilder.Entity<Course>()
                 .HasOne(x => x.secondTeacher)
                 .WithMany()
                 .OnDelete(DeleteBehavior.NoAction);

             modelBuilder.Entity<Course>()
                 .HasOne(x => x.firstTeacher)
                 .WithMany()
                 .OnDelete(DeleteBehavior.NoAction);*//*
            
            modelBuilder.Entity<Course>()
            .HasOne<Teacher>(p => p.firstTeacher)
            .WithMany(p => p.coursesOne)
            .HasForeignKey(p => p.firstTeacherId);
            modelBuilder.Entity<Course>()
            .HasOne<Teacher>(p => p.secondTeacher)
            .WithMany(p => p.coursesTwo)
            .HasForeignKey(p => p.secondTeacherId);



            modelBuilder.Entity<Enrollment>()
             .HasOne<Student>(p => p.student)
             .WithMany(p => p.enrollments)
             .HasForeignKey(p => p.studentId);
            //.HasPrincipalKey(p => p.Id);
            modelBuilder.Entity<Enrollment>()
            .HasOne<Course>(p => p.course)
            .WithMany(p => p.enrollments)
            .HasForeignKey(p => p.courseId);
            //.HasPrincipalKey(p => p.Id);*/
           
        //}
    }
}
