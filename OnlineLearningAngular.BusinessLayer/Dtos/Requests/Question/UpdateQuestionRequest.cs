using System.ComponentModel.DataAnnotations;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question
{
    public class UpdateQuestionRequest
    {
        public QuestionType Type { get; set; }
        
        [Required]
        public string QuestionName { get; set; } = string.Empty;
    }
}
