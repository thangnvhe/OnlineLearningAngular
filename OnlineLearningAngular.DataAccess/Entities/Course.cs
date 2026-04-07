using OnlineLearningAngular.DataAccess.Entities.Base;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public CourseType CourseType { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ApplicationUser? Author { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Module>? Modules { get; set; }
    }
}
