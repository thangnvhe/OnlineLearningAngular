import { EnrollmentStatus } from './enums.model';

export interface Enrollment {
  id: number; // Thêm Id định danh cho bản ghi đăng ký
  studentId: number; // ID của học viên
  courseId: number; // ID của khóa học

  enrollDate: string | Date;
  status: EnrollmentStatus; // Enum: NotStarted, InProgress, Completed...
  progress: number; // decimal (0-100%) -> number
  expiryDate?: string | Date; // Ngày hết hạn khóa học (Nullable)
}
