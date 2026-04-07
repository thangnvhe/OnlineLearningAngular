using OnlineLearningAngular.DataAccess.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class Enrollment 
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollDate { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.NotStarted;
        public decimal Progress { get; set; } 
        public DateTime? ExpiryDate { get; set; } 
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
        [ForeignKey("StudentId")]
        public ApplicationUser? Student { get; set; }
    }
}
