using OnlineLearningAngular.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class StudentExamDetail : BaseEntity<int>
    {
        public int StudentExamId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
        [ForeignKey("StudentExamId")]
        public StudentExam? StudentExam { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
        [ForeignKey("SelectedOptionId")]
        public QuestionOptions? SelectedOption { get; set; }

    }
}
