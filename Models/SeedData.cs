using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Data;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new School_Management_System_ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<School_Management_System_ApplicationContext>>()))
            {
                if (context.Course.Any() || context.Student.Any() || context.Teacher.Any())
                {
                    return;
                }

                context.Student.AddRange(
                    new Student
                    {
                        studentId = "139-2019",
                        firstName = "Stojan",
                        lastName = "Stojanoski",
                        enrollmentDate = DateTime.Parse("2019-4-20"),
                        acquiredCredits = 30,
                        currentSemester = 2,
                        educationLevel = "Bachelor's Degree"
                    },
                    new Student
                    {
                        studentId = "55-2019",
                        firstName = "Dimitar",
                        lastName = "Dimitrijoski",
                        enrollmentDate = DateTime.Parse("2019-7-11"),
                        acquiredCredits = 14,
                        currentSemester = 2,
                        educationLevel = "Bachelor's Degree"
                    },
                    new Student
                    {
                        studentId = "02-2016",
                        firstName = "Petar",
                        lastName = "Petreski",
                        enrollmentDate = DateTime.Parse("2020-2-4"),
                        acquiredCredits = 142,
                        currentSemester = 5,
                        educationLevel = "Bachelor's Degree"
                    },
                    new Student
                    {
                        studentId = "24-2020",
                        firstName = "Marija",
                        lastName = "Mitreska",
                        enrollmentDate = DateTime.Parse("2022-10-23"),
                        acquiredCredits = 210,
                        currentSemester = 8,
                        educationLevel = "Bachelor's Degree"
                    },
                    new Student
                    {
                        studentId = "20-2019",
                        firstName = "Ilina",
                        lastName = "Naumoska",
                        enrollmentDate = DateTime.Parse("2021-1-20"),
                        acquiredCredits = 78,
                        currentSemester = 3,
                        educationLevel = "Bachelor's Degree"
                    }
                );
                context.Teacher.AddRange(
                    new Teacher
                    {
                        firstName = "Simeon",
                        lastName = "Milenkoski",
                        degree = "Ph.D.",
                        academicRank = "Full Professor",
                        officeNumber = "223",
                        hireDate = DateTime.Parse("2002-3-20")
                    },
                    new Teacher
                    {
                        firstName = "Katerina",
                        lastName = "Stojkoska",
                        degree = "Master's degree",
                        academicRank = "Assistant Professor",
                        officeNumber = "211",
                        hireDate = DateTime.Parse("2010-2-11")
                    },
                    new Teacher
                    {
                        firstName = "Bojan",
                        lastName = "Nedelkoski",
                        degree = "Ph.D.",
                        academicRank = "Full Professor",
                        officeNumber = "102",
                        hireDate = DateTime.Parse("1998-1-11")
                    },
                    new Teacher
                    {
                        firstName = "Nebojsha",
                        lastName = "Ilijoski",
                        degree = "Master's degree",
                        academicRank = "Assistant Professor",
                        officeNumber = "204A",
                        hireDate = DateTime.Parse("2016-4-8")
                    }
                );
                context.SaveChanges();

                context.Course.AddRange(
                    new Course
                    {
                        title = "Matematika 1",
                        credits = 7,
                        semester = 1,
                        programme = "KTI",
                        educationLevel = "Bachelor's degree",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska").teacherId,
                        firstTeacher = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Bojan" && d.lastName == "Nedelkoski").teacherId,
                        secondTeacher = context.Teacher.Single(d => d.firstName == "Bojan" && d.lastName == "Nedelkoski")
                    }, 
                    new Course
                    {
                        title = "RSWEB",
                        credits = 6,
                        semester = 6,
                        programme = "KTI",
                        educationLevel = "Bachelor's degree",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska").teacherId,
                        firstTeacher = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Nebojsha" && d.lastName == "Ilijoski").teacherId,
                        secondTeacher = context.Teacher.Single(d => d.firstName == "Nebojsha" && d.lastName == "Ilijoski")
                    },
                    new Course
                    {
                        title = "Android programming",
                        credits = 6,
                        semester = 7,
                        programme = "KTI",
                        educationLevel = "Bachelor's degree",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Simeon" && d.lastName == "Milenkoski").teacherId,
                        firstTeacher = context.Teacher.Single(d => d.firstName == "Simeon" && d.lastName == "Milenkoski"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Nebojsha" && d.lastName == "Ilijoski").teacherId,
                        secondTeacher = context.Teacher.Single(d => d.firstName == "Nebojsha" && d.lastName == "Ilijoski")
                    }, 
                    new Course
                    {
                        title = "OWEB",
                        credits = 6,
                        semester = 5,
                        programme = "KTI",
                        educationLevel = "Bachelor's degree",
                        firstTeacherId = context.Teacher.Single(d => d.firstName == "Simeon" && d.lastName == "Milenkoski").teacherId,
                        firstTeacher = context.Teacher.Single(d => d.firstName == "Simeon" && d.lastName == "Milenkoski"),
                        secondTeacherId = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska").teacherId,
                        secondTeacher = context.Teacher.Single(d => d.firstName == "Katerina" && d.lastName == "Stojkoska")
                    }
                );
                context.SaveChanges();

                context.Enrollment.AddRange(
                    new Enrollment
                    {
                        courseId = context.Course.Single(d => d.title == "OWEB").courseId,
                        studentId = context.Student.Single(d => d.firstName == "Dimitar" && d.lastName == "Dimitrijoski").Id,
                        semester = 1,
                        year = 2021,
                        grade = 8,
                        seminalUrl = "github",
                        projectUrl = "github",
                        examPoints = 40,
                        seminalPoints = 20,
                        projectPoints = 20,
                        additionalPoints = 0,
                        finishDate = DateTime.Parse("2022-10-6")
                    }, 
                    new Enrollment
                    {
                        courseId = context.Course.Single(d => d.title == "OWEB").courseId,
                        studentId = context.Student.Single(d => d.firstName == "Petar" && d.lastName == "Petreski").Id,
                        semester = 1,
                        year = 2021,
                        grade = 6,
                        seminalUrl = "github",
                        projectUrl = "github",
                        examPoints = 20,
                        seminalPoints = 10,
                        projectPoints = 10,
                        additionalPoints = 10,
                        finishDate = DateTime.Parse("2022-10-6")
                    }, 
                    new Enrollment
                    {
                        courseId = context.Course.Single(d => d.title == "RSWEB").courseId,
                        studentId = context.Student.Single(d => d.firstName == "Stojan" && d.lastName == "Stojanoski").Id,
                        semester = 6,
                        year = 2021,
                        grade = 10,
                        seminalUrl = "github",
                        projectUrl = "github",
                        examPoints = 100,
                        seminalPoints = 20,
                        projectPoints = 20,
                        additionalPoints = 0,
                        finishDate = DateTime.Parse("2022-10-6")
                    }, 
                    new Enrollment
                    {
                        courseId = context.Course.Single(d => d.title == "OWEB").courseId,
                        studentId = context.Student.Single(d => d.firstName == "Ilina" && d.lastName == "Naumoska").Id,
                        semester = 7,
                        year = 2021,
                        grade = 9,
                        seminalUrl = "github",
                        projectUrl = "github",
                        examPoints = 60,
                        seminalPoints = 20,
                        projectPoints = 20,
                        additionalPoints = 5,
                        finishDate = DateTime.Parse("2022-10-6")
                    }, 
                    new Enrollment
                    {
                        courseId = context.Course.Single(d => d.title == "Matematika 1").courseId,
                        studentId = context.Student.Single(d => d.firstName == "Marija" && d.lastName == "Mitreska").Id,
                        semester = 7,
                        year = 2021,
                        grade = 5,
                        seminalUrl = "github",
                        projectUrl = "github",
                        examPoints = 10,
                        seminalPoints = 0,
                        projectPoints = 0,
                        additionalPoints = 0,
                        finishDate = DateTime.Parse("2022-10-6")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
