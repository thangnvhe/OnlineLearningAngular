using Microsoft.AspNetCore.Identity;
using OnlineLearningAngular.DataAccess.Entities.Base;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser<int>, IEntity<int>
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public bool IsMale { get; set; } = true;
        public bool IsActive { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? AvatarUrl { get; set; }
        public ICollection<Course>? AuthorCourses { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<StudentExam>? StudentExams { get; set; }
    }
}
