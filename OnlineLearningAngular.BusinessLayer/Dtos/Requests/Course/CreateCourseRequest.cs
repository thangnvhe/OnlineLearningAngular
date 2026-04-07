using System.ComponentModel.DataAnnotations;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course
{
    public class CreateCourseRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public CourseType CourseType { get; set; }
        public CourseStatus CourseStatus { get; set; }
    }
}
