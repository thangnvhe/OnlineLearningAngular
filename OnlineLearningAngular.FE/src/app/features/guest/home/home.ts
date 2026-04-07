import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { BadgeModule } from 'primeng/badge';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule, 
    ButtonModule, 
    InputTextModule, 
    CardModule, 
    BadgeModule,
    RippleModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Home {}
