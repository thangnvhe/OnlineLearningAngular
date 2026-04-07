import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, InputTextModule],
  templateUrl: './forgot-password.html',
})
export class ForgotPassword {
  private readonly authService = inject(AuthService);

  model = {
    email: '',
  };

  loading = false;
  submitted = false;
  successMessage = '';
  errorMessage = '';

  onSubmit(form: NgForm): void {
    this.submitted = true;
    this.errorMessage = '';
    this.successMessage = '';

    if (form.invalid || this.loading) {
      return;
    }

    this.loading = true;
    this.authService.forgotPassword(this.model).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.successMessage = 'If your email exists, a reset link has been sent.';
          form.resetForm({ email: '' });
          this.submitted = false;
        } else {
          this.errorMessage = res.errorMessages?.join(' ') || 'Failed to send reset email.';
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
