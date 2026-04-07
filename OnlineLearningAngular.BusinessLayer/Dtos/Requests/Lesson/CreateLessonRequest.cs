using System.ComponentModel.DataAnnotations;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson
{
    public class CreateLessonRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ModuleId { get; set; }
    }
}
