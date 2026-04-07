namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class LessonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ModuleId { get; set; }
    }
}
