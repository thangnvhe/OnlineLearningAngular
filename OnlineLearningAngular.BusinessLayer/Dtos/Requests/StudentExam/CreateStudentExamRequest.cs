using System.ComponentModel.DataAnnotations;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam
{
    public class CreateStudentExamRequest
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; } 
        public bool IsPassed { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
