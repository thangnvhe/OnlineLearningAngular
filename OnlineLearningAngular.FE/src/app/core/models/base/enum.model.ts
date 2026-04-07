export enum CourseStatus {
  /** 0. Đang soạn thảo: Chỉ giảng viên/Admin thấy */
  Draft = 0,

  /** 1. Chờ duyệt: Cần Admin kiểm duyệt */
  PendingApproval = 1,

  /** 2. Đã xuất bản: Hiển thị công khai */
  Published = 2,

  /** 3. Tạm ẩn: Không cho đăng ký mới, người cũ vẫn học được */
  Hidden = 3,

  /** 4. Lưu trữ: Ẩn hoàn toàn khỏi hệ thống */
  Archived = 4,
}

export enum CourseType {
  Programming = 1,
  Design = 2,
  Marketing = 3,
  Business = 4,
  PersonalDevelopment = 5,
  Photography = 6,
  Music = 7,
  HealthAndFitness = 8,
  LanguageLearning = 9,
  TestPreparation = 10,
}

export enum LogType {
  Information = 1, // Nhớ check lại giá trị int bên C# của ông nhé
  Warning = 2,
  Error = 3,
}

export enum EnrollmentStatus {
  NotStarted = 0,
  InProgress = 1,
  Completed = 2,
  Expired = 3,
}
