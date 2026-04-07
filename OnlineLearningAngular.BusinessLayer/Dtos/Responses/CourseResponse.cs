using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public CourseType CourseType { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
