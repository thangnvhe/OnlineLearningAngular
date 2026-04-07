using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Enums
{
    public enum ModuleStatus
    {
        // 0. Đang soạn thảo: Module mới tạo khung, chưa có bài học hoặc bài học chưa xong.
        Draft = 0,

        // 1. Hoàn thiện: Nội dung đã đầy đủ, sẵn sàng cho học viên.
        Active = 1,

        // 2. Sắp ra mắt (Coming Soon): Hiển thị tên Module để "nhá hàng" nhưng chưa cho bấm vào xem.
        ComingSoon = 2,

        // 3. Tạm ẩn: Bảo trì hoặc cập nhật nội dung bài học bên trong.
        Hidden = 3
    }
}
