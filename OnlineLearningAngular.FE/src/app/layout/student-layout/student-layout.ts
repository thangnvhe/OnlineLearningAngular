import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-student-layout',
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './student-layout.html',
  styleUrl: './student-layout.css',
})
export class StudentLayout {
    menuItems = [
    { label: 'Dashboard', icon: 'pi pi-th-large', route: '/teacher/dashboard' },
    { label: 'Hồ sơ của tôi', icon: 'pi pi-user', route: '/teacher/profile' },
    { label: 'Khóa học', icon: 'pi pi-book', route: '/teacher/courses' },
    { label: 'Thông báo', icon: 'pi pi-bell', route: '/teacher/notifications' },
    { label: 'Bài tập', icon: 'pi pi-file-edit', route: '/teacher/assignments' },
    { label: 'Học viên', icon: 'pi pi-users', route: '/teacher/students' },
    { label: 'Trắc nghiệm', icon: 'pi pi-question-circle', route: '/teacher/quizzes' },
  ];
}
