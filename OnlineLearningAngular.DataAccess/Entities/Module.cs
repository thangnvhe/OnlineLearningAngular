using OnlineLearningAngular.DataAccess.Entities.Base;
using OnlineLearningAngular.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class Module : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ModuleStatus ModuleStatus { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        public ICollection<Exam>? Exams { get; set; }
    }
}
