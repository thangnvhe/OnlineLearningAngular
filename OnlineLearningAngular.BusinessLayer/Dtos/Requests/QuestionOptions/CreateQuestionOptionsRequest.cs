using System.ComponentModel.DataAnnotations;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions
{
    public class CreateQuestionOptionsRequest
    {
        [Required]
        public string OptionName { get; set; } = string.Empty;
        public bool? IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
