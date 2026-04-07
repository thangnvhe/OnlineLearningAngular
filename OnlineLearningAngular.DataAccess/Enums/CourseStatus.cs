using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Enums
{
    public enum CourseStatus
    {
        // 0. Đang soạn thảo: Chỉ giảng viên/Admin thấy, chưa cho phép đăng ký.
        Draft = 0,

        // 1. Chờ duyệt: Giảng viên làm xong nhưng cần Admin kiểm duyệt nội dung.
        PendingApproval = 1,

        // 2. Đã xuất bản: Khóa học hiển thị công khai, học viên có thể vào mua/học.
        Published = 2,

        // 3. Tạm ẩn: Không cho đăng ký mới, nhưng những người đã mua rồi vẫn học được.
        Hidden = 3,

        // 4. Lưu trữ: Khóa học cũ, không còn giá trị, ẩn hoàn toàn khỏi hệ thống.
        Archived = 4
    }
}
