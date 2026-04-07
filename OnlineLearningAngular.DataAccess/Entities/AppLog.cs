using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.DataAccess.Entities
{
    public class AppLog
    {
        public int Id { get; set; }
        public LogType Level { get; set; } = LogType.Infomation; // Info, Error, Warning
        public string Source { get; set; } // Tên Controller/Service
        public string Action { get; set; } // Tên hàm (Login, Update,...)
        public string Message { get; set; } // Nội dung thông báo
        public string? StackTrace { get; set; } // Vết lỗi (chỉ dùng cho Error)
        public int? UserId { get; set; } 
        public string? IPAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
