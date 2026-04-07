using OnlineLearningAngular.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class QuestionOptions : BaseEntity<int>
    {
        public string OptionName { get; set; }
        public bool? IsCorrect { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
    }
}
