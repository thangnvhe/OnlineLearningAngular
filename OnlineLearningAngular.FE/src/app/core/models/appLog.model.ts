import { LogType } from './base/enum.model';

export interface AppLog {
  id: number;
  level: LogType; // Enum: Information, Error, Warning
  source: string; // Tên Controller/Service
  action: string; // Tên hàm (Login, Update,...)
  message: string; // Nội dung thông báo
  stackTrace?: string; // Vết lỗi (Nullable)
  userId?: number; // ID người dùng gây ra log (Nullable)
  ipAddress?: string; // Địa chỉ IP (Nullable)
  createdAt: string | Date; // Mặc định DateTime.Now từ server
}
