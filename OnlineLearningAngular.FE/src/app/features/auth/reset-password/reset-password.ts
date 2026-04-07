import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, InputTextModule, PasswordModule],
  templateUrl: './reset-password.html',
})
export class ResetPassword {
  private readonly authService = inject(AuthService);
  private readonly route = inject(ActivatedRoute);

  model = {
    userId: '',
    token: '',
    newPassword: '',
  };

  loading = false;
  submitted = false;
  successMessage = '';
  errorMessage = '';

  constructor() {
    this.model.userId = this.route.snapshot.queryParamMap.get('userId') ?? '';
    this.model.token = this.route.snapshot.queryParamMap.get('token') ?? '';
  }

  onSubmit(form: NgForm): void {
    this.submitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (form.invalid || this.loading) {
      return;
    }

    this.loading = true;
    this.authService.resetPassword(this.model).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.successMessage =
            'Password reset successfully. You can now sign in with your new password.';
          this.model.newPassword = '';
          this.submitted = false;
          form.control.get('newPassword')?.reset();
        } else {
          this.errorMessage = res.errorMessages?.join(' ') || 'Unable to reset password.';
        }
      },
      error: () => {
        this.errorMessage = 'Unable to process your request right now. Please try again later.';
      },
      complete: () => {
        this.loading = false;
      },
    });
  }
}
