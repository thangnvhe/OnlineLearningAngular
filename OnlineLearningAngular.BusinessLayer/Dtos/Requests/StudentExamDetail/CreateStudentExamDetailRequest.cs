using System.ComponentModel.DataAnnotations;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail
{
    public class CreateStudentExamDetailRequest
    {
        public int StudentExamId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
