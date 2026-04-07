using OnlineLearningAngular.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class StudentExam : BaseEntity<int>
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public double Score { get; set; } 
        public bool IsPassed { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [ForeignKey("ExamId")]
        public Exam? Exam { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser? Student { get; set; }
        public ICollection<StudentExamDetail>? ExamDetails { get; set; }
    }
}
