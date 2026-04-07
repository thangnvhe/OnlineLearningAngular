import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-resend-confirm-email',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink, InputTextModule],
  templateUrl: './resend-confirm-email.html',
})
export class ResendConfirmEmail {
  private readonly authService = inject(AuthService);

  model = {
    username: '',
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
    this.authService.resendConfirmationEmail(this.model).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.successMessage = 'Confirmation email has been sent. Please check your inbox.';
          this.submitted = false;
        } else {
          this.errorMessage =
            res.errorMessages?.join(' ') || 'Unable to resend confirmation email.';
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
