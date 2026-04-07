import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    RouterLink, 
    ButtonModule, 
    InputTextModule, 
    PasswordModule,
    RippleModule
  ],
  templateUrl: './login.html',
})
export class Login {}
