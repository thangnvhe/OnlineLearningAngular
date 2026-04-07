using OnlineLearningAngular.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class Exam : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ModuleId { get; set; }
        [ForeignKey("ModuleId")]
        public Module? Module { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}
