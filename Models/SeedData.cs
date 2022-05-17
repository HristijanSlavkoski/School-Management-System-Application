using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School_Management_System_Application.Areas.Identity.Data;
using School_Management_System_Application.Data;
using School_Management_System_Application.Models;

namespace School_Management_System_Application.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            User user = await UserManager.FindByEmailAsync("admin@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "admin@school.com";
                User.UserName = "admin@school.com";
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
            //Add Teacher Role
            roleCheck = await RoleManager.RoleExistsAsync("Teacher");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Teacher")); }
            user = await UserManager.FindByEmailAsync("simeon.milenkoski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "simeon.milenkoski@school.com";
                User.UserName = "simeon.milenkoski@school.com";
                string userPWD = "Teacher123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Teacher
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Teacher"); }
            }
            user = await UserManager.FindByEmailAsync("katerina.stojkoska@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "katerina.stojkoska@school.com";
                User.UserName = "katerina.stojkoska@school.com";
                string userPWD = "Teacher123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Teacher
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Teacher"); }
            }
            user = await UserManager.FindByEmailAsync("bojan.nedelkoski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "bojan.nedelkoski@school.com";
                User.UserName = "bojan.nedelkoski@school.com";
                string userPWD = "Teacher123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Teacher
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Teacher"); }
            }
            user = await UserManager.FindByEmailAsync("nebojsha.ilijoski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "nebojsha.ilijoski@school.com";
                User.UserName = "nebojsha.ilijoski@school.com";
                string userPWD = "Teacher123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Teacher
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Teacher"); }
            }
            //Add Student Role
            roleCheck = await RoleManager.RoleExistsAsync("Student");
            if (!roleCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Student")); }
            user = await UserManager.FindByEmailAsync("stojan.stojanoski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "stojan.stojanoski@school.com";
                User.UserName = "stojan.stojanoski@school.com";
                string userPWD = "Student123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Student
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Student"); }
            }
            user = await UserManager.FindByEmailAsync("dimitar.dimitrijoski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "dimitar.dimitrijoski@school.com";
                User.UserName = "dimitar.dimitrijoski@school.com";
                string userPWD = "Student123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Student
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Student"); }
            }
            user = await UserManager.FindByEmailAsync("marija.mitreska@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "marija.mitreska@school.com";
                User.UserName = "marija.mitreska@school.com";
                string userPWD = "Student123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Student
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Student"); }
            }
            user = await UserManager.FindByEmailAsync("petar.petreski@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "petar.petreski@school.com";
                User.UserName = "petar.petreski@school.com";
                string userPWD = "Student123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Student
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Student"); }
            }
            user = await UserManager.FindByEmailAsync("ilina.naumoska@school.com");
            if (user == null)
            {
                var User = new User();
                User.Email = "ilina.naumoska@school.com";
                User.UserName = "ilina.naumoska@school.com";
                string userPWD = "Student123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Student
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Student"); }
            }
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new School_Management_System_ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<School_Management_System_ApplicationContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                

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
                        educationLevel = "Bachelor's Degree",
                        userIdentityId = context.Users.Single(x => x.Email == "stojan.stojanoski@school.com").Id
                    },
                    new Student
                    {
                        studentId = "55-2019",
                        firstName = "Dimitar",
                        lastName = "Dimitrijoski",
                        enrollmentDate = DateTime.Parse("2019-7-11"),
                        acquiredCredits = 14,
                        currentSemester = 2,
                        educationLevel = "Bachelor's Degree",
                        userIdentityId = context.Users.Single(x => x.Email == "dimitar.dimitrijoski@school.com").Id
                    },
                    new Student
                    {
                        studentId = "02-2016",
                        firstName = "Petar",
                        lastName = "Petreski",
                        enrollmentDate = DateTime.Parse("2020-2-4"),
                        acquiredCredits = 142,
                        currentSemester = 5,
                        educationLevel = "Bachelor's Degree",
                        userIdentityId = context.Users.Single(x => x.Email == "petar.petreski@school.com").Id
                    },
                    new Student
                    {
                        studentId = "24-2020",
                        firstName = "Marija",
                        lastName = "Mitreska",
                        enrollmentDate = DateTime.Parse("2022-10-23"),
                        acquiredCredits = 210,
                        currentSemester = 8,
                        educationLevel = "Bachelor's Degree",
                        userIdentityId = context.Users.Single(x => x.Email == "marija.mitreska@school.com").Id
                    },
                    new Student
                    {
                        studentId = "20-2019",
                        firstName = "Ilina",
                        lastName = "Naumoska",
                        enrollmentDate = DateTime.Parse("2021-1-20"),
                        acquiredCredits = 78,
                        currentSemester = 3,
                        educationLevel = "Bachelor's Degree",
                        userIdentityId = context.Users.Single(x => x.Email == "ilina.naumoska@school.com").Id
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
                        hireDate = DateTime.Parse("2002-3-20"),
                        userIdentityId = context.Users.Single(x => x.Email == "simeon.milenkoski@school.com").Id
                    },
                    new Teacher
                    {
                        firstName = "Katerina",
                        lastName = "Stojkoska",
                        degree = "Master's degree",
                        academicRank = "Assistant Professor",
                        officeNumber = "211",
                        hireDate = DateTime.Parse("2010-2-11"),
                        userIdentityId = context.Users.Single(x => x.Email == "katerina.stojkoska@school.com").Id
                    },
                    new Teacher
                    {
                        firstName = "Bojan",
                        lastName = "Nedelkoski",
                        degree = "Ph.D.",
                        academicRank = "Full Professor",
                        officeNumber = "102",
                        hireDate = DateTime.Parse("1998-1-11"),
                        userIdentityId = context.Users.Single(x => x.Email == "bojan.nedelkoski@school.com").Id
                    },
                    new Teacher
                    {
                        firstName = "Nebojsha",
                        lastName = "Ilijoski",
                        degree = "Master's degree",
                        academicRank = "Assistant Professor",
                        officeNumber = "204A",
                        hireDate = DateTime.Parse("2016-4-8"),
                        userIdentityId = context.Users.Single(x => x.Email == "nebojsha.ilijoski@school.com").Id
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
                        semester = "zemski",
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
                        semester = "zimski",
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
                        semester = "leten",
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
                        semester = "zimski",
                        year = 2020,
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
                        semester = "zimski",
                        year = 2019,
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
