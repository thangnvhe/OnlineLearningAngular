using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Common
{
    public static class Permissions
    {
        public static class Courses
        {
            public const string View = "Permissions.Courses.View";
            public const string Create = "Permissions.Courses.Create";
            public const string Edit = "Permissions.Courses.Edit";
            public const string Delete = "Permissions.Courses.Delete";
        }

        public static class Exams
        {
            public const string View = "Permissions.Exams.View";
            public const string Create = "Permissions.Exams.Create";
            public const string Edit = "Permissions.Exams.Edit";
            public const string Delete = "Permissions.Exams.Delete";
        }

        public static class Lessons
        {
            public const string View = "Permissions.Lessons.View";
            public const string Create = "Permissions.Lessons.Create";
            public const string Edit = "Permissions.Lessons.Edit";
            public const string Delete = "Permissions.Lessons.Delete";
        }

        public static class Modules
        {
            public const string View = "Permissions.Modules.View";
            public const string Create = "Permissions.Modules.Create";
            public const string Edit = "Permissions.Modules.Edit";
            public const string Delete = "Permissions.Modules.Delete";
        }

        public static class Questions
        {
            public const string View = "Permissions.Questions.View";
            public const string Create = "Permissions.Questions.Create";
            public const string Edit = "Permissions.Questions.Edit";
            public const string Delete = "Permissions.Questions.Delete";
        }

        public static class QuestionOptions
        {
            public const string View = "Permissions.QuestionOptions.View";
            public const string Create = "Permissions.QuestionOptions.Create";
            public const string Edit = "Permissions.QuestionOptions.Edit";
            public const string Delete = "Permissions.QuestionOptions.Delete";
        }

        public static class StudentExams
        {
            public const string View = "Permissions.StudentExams.View";
            public const string Create = "Permissions.StudentExams.Create";
            public const string Edit = "Permissions.StudentExams.Edit";
            public const string Delete = "Permissions.StudentExams.Delete";
        }

        public static class StudentExamDetails
        {
            public const string View = "Permissions.StudentExamDetails.View";
            public const string Create = "Permissions.StudentExamDetails.Create";
            public const string Edit = "Permissions.StudentExamDetails.Edit";
            public const string Delete = "Permissions.StudentExamDetails.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }
        public static List<string> GetAllPermissions()
        {
            return typeof(Permissions).GetNestedTypes()
                .SelectMany(x => x.GetFields())
                .Select(x => x.GetValue(null)?.ToString() ?? "")
                .ToList();
        }
    }
}
