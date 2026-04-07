import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, signal } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Dashboard {
  currentView = signal<'dashboard' | 'detail'>('dashboard');

  menuItems = [
    { label: 'Dashboard', icon: 'pi pi-th-large', view: 'dashboard' as const },
    { label: 'Hồ sơ của tôi', icon: 'pi pi-user', view: 'dashboard' as const },
    { label: 'Khóa học', icon: 'pi pi-book', view: 'dashboard' as const },
    { label: 'Thông báo', icon: 'pi pi-bell', view: 'dashboard' as const },
    { label: 'Bài tập', icon: 'pi pi-file-edit', view: 'dashboard' as const },
    { label: 'Học viên', icon: 'pi pi-users', view: 'dashboard' as const },
    { label: 'Trắc nghiệm', icon: 'pi pi-question-circle', view: 'dashboard' as const },
  ];

  stats = [
    {
      label: 'Khóa học đăng ký',
      value: '12',
      icon: 'pi pi-book',
      colorClass: 'bg-indigo-50 text-indigo-500',
    },
    {
      label: 'Khóa học hoạt động',
      value: '08',
      icon: 'pi pi-video',
      colorClass: 'bg-red-50 text-red-400',
    },
    {
      label: 'Khóa học hoàn thành',
      value: '06',
      icon: 'pi pi-check-circle',
      colorClass: 'bg-green-50 text-green-500',
    },
    {
      label: 'Tổng số học viên',
      value: '17',
      icon: 'pi pi-users',
      colorClass: 'bg-blue-50 text-blue-500',
    },
    {
      label: 'Tổng số khóa học',
      value: '11',
      icon: 'pi pi-list',
      colorClass: 'bg-cyan-50 text-cyan-400',
    },
    {
      label: 'Tổng thu nhập',
      value: '$486',
      icon: 'pi pi-dollar',
      colorClass: 'bg-purple-50 text-purple-500',
    },
  ];

  earningsData = [
    { month: 'Jan', value: 80 },
    { month: 'Feb', value: 100 },
    { month: 'Mar', value: 70 },
    { month: 'Apr', value: 110 },
    { month: 'May', value: 80 },
    { month: 'Jun', value: 90 },
    { month: 'Jul', value: 85 },
    { month: 'Aug', value: 85 },
    { month: 'Sep', value: 110 },
    { month: 'Oct', value: 30 },
    { month: 'Nov', value: 100 },
    { month: 'Dec', value: 90 },
  ];

  recentCourses = [
    {
      id: 1,
      title: 'Complete HTML, CSS and Javascript Course',
      enrolled: 0,
      status: 'PUBLISHED',
      image: 'https://images.unsplash.com/photo-1498050108023-c5249f4df085?w=400',
    },
    {
      id: 2,
      title: 'Complete Course on Fullstack Web Developer',
      enrolled: 2,
      status: 'PUBLISHED',
      image: 'https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=400',
    },
  ];

  ngOnInit() {
    // Initialization logic if needed
  }
}
