using OnlineLearningAngular.DataAccess.Entities.Base;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class Question : BaseEntity<int>
    {
        public QuestionType Type { get; set; }
        public string QuestionName { get; set; }
        public int ExamId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ExamId")]
        public Exam? Exam { get; set; }
        public ICollection<QuestionOptions>? QuestionOptions { get; set; }
    }
}
