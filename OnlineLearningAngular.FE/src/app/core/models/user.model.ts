export interface User {
  id: number;
  userName: string;
  email: string;
  phoneNumber?: string; // Mặc định của IdentityUser

  fullName: string;
  dob: string | Date; // DateTime từ API thường là string ISO
  address: string;
  isMale: boolean;
  isActive: boolean;

  avatarUrl?: string; // Dấu ? tương đương với string? (nullable)
  createAt?: string | Date;
  updateAt?: string | Date;
}
