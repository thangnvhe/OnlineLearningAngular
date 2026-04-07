namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class StudentExamDetailResponse
    {
        public int Id { get; set; }
        public int StudentExamId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
