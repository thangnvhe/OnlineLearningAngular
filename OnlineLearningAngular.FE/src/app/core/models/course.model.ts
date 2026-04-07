import { CourseStatus, CourseType } from './base/enum.model'; // Nếu ông để Enum ở file riêng

export interface Course {
  id: number; // Thêm Id (thường là int bên C# nên để number)
  name: string;
  description?: string; // string? tương ứng với optional trong TS
  imageUrl?: string;
  avatar?: string;
  discount?: string;
  price: number; // decimal bên C# map thành number bên TS
  authorId: number;
  authorName: string;
  // Đối với Enum, ông có thể để number hoặc tạo Enum tương ứng trong TS
  courseType: CourseType;
  courseStatus: CourseStatus;
  rating?: number; // float/double bên C# cũng map thành number
  reviews?: number; // int bên C# map thành number
  // DateTime bên C# khi gửi qua JSON sẽ là chuỗi String (ISO 8601)
  createAt: string | Date;
  updateAt: string | Date;
}
