using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class ModuleResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ModuleStatus ModuleStatus { get; set; }
        public int CourseId { get; set; }
    }
}
