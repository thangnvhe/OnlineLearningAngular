namespace OnlineLearningAngular.BusinessLayer.Dtos.Responses
{
    public class StudentExamResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; } 
        public bool IsPassed { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
