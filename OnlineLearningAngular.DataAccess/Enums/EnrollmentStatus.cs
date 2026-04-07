using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Enums
{
    public enum EnrollmentStatus
    {
        NotStarted = 0,    // Chưa học
        InLearning = 1,    // Đang học
        Completed = 2,     // Đã hoàn thành (Progress = 100%)
        Expired = 3
    }
}
