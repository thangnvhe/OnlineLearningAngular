import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { MenubarModule } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { BadgeModule } from 'primeng/badge';
import { AvatarModule } from 'primeng/avatar';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule, 
    RouterLink, 
    MenubarModule, 
    ButtonModule, 
    BadgeModule, 
    AvatarModule, 
    InputTextModule, 
    RippleModule
  ],
  templateUrl: './header.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Header implements OnInit {
  private router = inject(Router);
  items: MenuItem[] | undefined;

  ngOnInit() {
    this.items = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: '/',
      },
      {
        label: 'Courses',
        icon: 'pi pi-book',
        routerLink: '/courses',
      },
      {
        label: 'Teachers',
        icon: 'pi pi-users',
        routerLink: '/teachers',
      },
      {
        label: 'Blog',
        icon: 'pi pi-comment',
        routerLink: '/blog',
      },
      {
        label: 'Contact',
        icon: 'pi pi-envelope',
      },
    ];
  }

  navigateToSignIn() {
    this.router.navigate(['/login']);
  }

  navigateToSignUp() {
    this.router.navigate(['/register']);
  }
}
