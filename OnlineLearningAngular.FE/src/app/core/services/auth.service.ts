import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { BaseResponse } from '../models/base/base.model';

export interface ForgotPasswordRequest {
  email: string;
}

export interface ResendConfirmationEmailRequest {
  username: string;
}

export interface ResetPasswordRequest {
  userId: string;
  token: string;
  newPassword: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private readonly apiUrl = `${environment.apiUrl}/auth`; // Giả sử endpoint là /api/auth

  // Giống như State trong Zustand
  private currentUserSubject = new BehaviorSubject<any>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    // Khi khởi tạo, kiểm tra xem có token cũ trong máy không
    const token = this.getToken();
    if (token) {
      // Có thể giải mã token hoặc gọi API GetProfile ở đây
      this.currentUserSubject.next({ token });
    }
  }

  // --- QUẢN LÝ TOKEN (Giống logic React của ông) ---
  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  saveToken(token: string): void {
    localStorage.setItem('accessToken', token);
  }

  // --- CÁC HÀM NGHIỆP VỤ ---

  // Đăng nhập
  login(credentials: any): Observable<BaseResponse<any>> {
    return this.http.post<BaseResponse<any>>(`${this.apiUrl}/login`, credentials).pipe(
      tap((res) => {
        if (res.isSuccess) {
          this.saveToken(res.result.accessToken);
          this.currentUserSubject.next(res.result.user);
        }
      }),
    );
  }

  // Refresh Token (Hàm này cực kỳ quan trọng cho Interceptor)
  refreshToken(): Observable<BaseResponse<any>> {
    // Với ASP.NET Core, thường mình gửi Refresh Token qua Cookie (withCredentials)
    // hoặc gửi kèm trong Body tùy ông cấu hình Backend
    return this.http.post<BaseResponse<any>>(`${this.apiUrl}/refresh-token`, {});
  }

  forgotPassword(request: ForgotPasswordRequest): Observable<BaseResponse<boolean>> {
    return this.http.post<BaseResponse<boolean>>(`${this.apiUrl}/forgotpassword`, request);
  }

  resendConfirmationEmail(
    request: ResendConfirmationEmailRequest,
  ): Observable<BaseResponse<boolean>> {
    return this.http.post<BaseResponse<boolean>>(`${this.apiUrl}/resendconfirmationemail`, request);
  }

  resetPassword(request: ResetPasswordRequest): Observable<BaseResponse<boolean>> {
    return this.http.post<BaseResponse<boolean>>(`${this.apiUrl}/resetpassword`, request);
  }

  // Đăng xuất
  logout(): void {
    localStorage.removeItem('accessToken');
    this.currentUserSubject.next(null);
    this.router.navigate(['/auth/login']);
  }

  // Kiểm tra trạng thái (Getter)
  get isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
