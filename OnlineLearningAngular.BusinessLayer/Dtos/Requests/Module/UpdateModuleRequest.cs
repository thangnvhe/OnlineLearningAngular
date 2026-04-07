using System.ComponentModel.DataAnnotations;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module
{
    public class UpdateModuleRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ModuleStatus ModuleStatus { get; set; }
        public int CourseId { get; set; }
    }
}
