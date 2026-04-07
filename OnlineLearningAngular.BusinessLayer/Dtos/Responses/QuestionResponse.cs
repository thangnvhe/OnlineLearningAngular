using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public QuestionType Type { get; set; }
        public string QuestionName { get; set; } = string.Empty;
    }
}
