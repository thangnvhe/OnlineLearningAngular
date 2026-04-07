namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class QuestionOptionsResponse
    {
        public int Id { get; set; }
        public string OptionName { get; set; } = string.Empty;
        public bool? IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
