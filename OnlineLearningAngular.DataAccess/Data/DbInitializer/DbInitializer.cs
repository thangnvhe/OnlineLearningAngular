using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public DbInitializer(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                    _context.Database.Migrate();
            }
            catch (Exception)
            {
                return;
            }
            if (!_roleManager.RoleExistsAsync(Utilizer.Role_Student).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole<int>(Utilizer.Role_SuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole<int>(Utilizer.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole<int>(Utilizer.Role_Teacher)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole<int>(Utilizer.Role_Student)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole<int>(Utilizer.Role_Supporter)).GetAwaiter().GetResult();

                // Generate Claims
                var allPermissions = Permissions.GetAllPermissions();

                // 1. SuperAdmin (All Permissions)
                var superAdminRole = _roleManager.FindByNameAsync(Utilizer.Role_SuperAdmin).GetAwaiter().GetResult();
                foreach (var permission in allPermissions)
                {
                    _roleManager.AddClaimAsync(superAdminRole!, new System.Security.Claims.Claim("Permission", permission)).GetAwaiter().GetResult();
                }

                // 2. Admin (All Permissions)
                var adminRole = _roleManager.FindByNameAsync(Utilizer.Role_Admin).GetAwaiter().GetResult();
                foreach (var permission in allPermissions)
                {
                    _roleManager.AddClaimAsync(adminRole!, new System.Security.Claims.Claim("Permission", permission)).GetAwaiter().GetResult();
                }

                // 3. Teacher (All Permissions except Users administration)
                var teacherRole = _roleManager.FindByNameAsync(Utilizer.Role_Teacher).GetAwaiter().GetResult();
                var teacherPermissions = allPermissions.Where(p => !p.Contains("Users")).ToList();
                foreach (var permission in teacherPermissions)
                {
                    _roleManager.AddClaimAsync(teacherRole!, new System.Security.Claims.Claim("Permission", permission)).GetAwaiter().GetResult();
                }

                // 4. Supporter (Only View Permissions)
                var supporterRole = _roleManager.FindByNameAsync(Utilizer.Role_Supporter).GetAwaiter().GetResult();
                var supporterPermissions = allPermissions.Where(p => p.EndsWith(".View")).ToList();
                foreach (var permission in supporterPermissions)
                {
                    _roleManager.AddClaimAsync(supporterRole!, new System.Security.Claims.Claim("Permission", permission)).GetAwaiter().GetResult();
                }

                // 5. Student (View courses/content, CRUD student exams)
                var studentRole = _roleManager.FindByNameAsync(Utilizer.Role_Student).GetAwaiter().GetResult();
                var studentPermissions = new List<string>
                {
                    Permissions.Courses.View,
                    Permissions.Exams.View,
                    Permissions.Lessons.View,
                    Permissions.Modules.View,
                    Permissions.Questions.View,
                    Permissions.QuestionOptions.View,
                    Permissions.StudentExams.View,
                    Permissions.StudentExams.Create,
                    Permissions.StudentExamDetails.View,
                    Permissions.StudentExamDetails.Create
                };
                foreach (var permission in studentPermissions)
                {
                    _roleManager.AddClaimAsync(studentRole!, new System.Security.Claims.Claim("Permission", permission)).GetAwaiter().GetResult();
                }
            }

            if (!_context.Courses.Any())
            {
                var adminUser = _context.Users.FirstOrDefault();
                
                var course1 = new Course
                {
                    Name = ".NET Core Web API Real World",
                    Description = "Learn Clean Architecture and EF Core from scratch.",
                    Price = 50.0m,
                    AuthorId = adminUser.Id,
                    CourseType = (OnlineLearningAngular.DataAccess.Enums.CourseType)1,
                    CourseStatus = (OnlineLearningAngular.DataAccess.Enums.CourseStatus)1,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.Courses.Add(course1);
                _context.SaveChanges();

                var module1 = new Module
                {
                    Name = "Chapter 1: Getting Started",
                    Description = "Introduction concepts.",
                    CourseId = course1.Id
                };
                _context.Modules.Add(module1);
                _context.SaveChanges();

                var lesson1 = new Lesson
                {
                    Name = "What is Clean Architecture?",
                    Description = "Basic understanding of layer decoupling.",
                    ModuleId = module1.Id
                };
                _context.Lessons.Add(lesson1);
                _context.SaveChanges();

                var exam1 = new Exam
                {
                    Name = "Chapter 1 Quiz",
                    Description = "Test your understanding of the introduction.",
                    ModuleId = module1.Id
                };
                _context.Exams.Add(exam1);
                _context.SaveChanges();

                var question1 = new Question
                {
                    QuestionName = "Which layer holds the business logic in Clean Architecture?",
                    Type = (OnlineLearningAngular.DataAccess.Enums.QuestionType)1,
                    ExamId = exam1.Id
                };
                _context.Questions.Add(question1);
                _context.SaveChanges();

                _context.QuestionOptions.AddRange(
                    new QuestionOptions { OptionName = "API Layer", IsCorrect = false, QuestionId = question1.Id },
                    new QuestionOptions { OptionName = "Infrastructure Layer", IsCorrect = false, QuestionId = question1.Id },
                    new QuestionOptions { OptionName = "Domain / Business Layer", IsCorrect = true, QuestionId = question1.Id }
                );
                _context.SaveChanges();
            }
        }
    }
}
