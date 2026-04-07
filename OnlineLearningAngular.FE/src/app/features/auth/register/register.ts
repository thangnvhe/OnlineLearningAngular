import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { RadioButtonModule } from 'primeng/radiobutton';
import { DatePickerModule } from 'primeng/datepicker';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterLink, 
    ButtonModule, 
    InputTextModule, 
    PasswordModule,
    RadioButtonModule,
    DatePickerModule,
    RippleModule
  ],
  templateUrl: './register.html',
})
export class Register {
  isMale: boolean = true;
  dob: Date | undefined;
}
