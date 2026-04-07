import { Component, OnInit } from '@angular/core';
import { ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { signal } from '@angular/core';

@Component({
  selector: 'app-view-courses',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-courses.html',
  styleUrl: './view-courses.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ViewCourses implements OnInit{
   currentView = signal<'dashboard' | 'courses' | 'detail'>('courses'); // Default to new view

  menuItems = [
    { label: 'Dashboard', icon: 'pi pi-th-large', view: 'dashboard' },
    { label: 'Hồ sơ của tôi', icon: 'pi pi-user', view: 'dashboard' },
    { label: 'Khóa học', icon: 'pi pi-book', view: 'courses' },
    { label: 'Thông báo', icon: 'pi pi-bell', view: 'dashboard' },
    { label: 'Bài tập', icon: 'pi pi-file-edit', view: 'dashboard' },
    { label: 'Học viên', icon: 'pi pi-users', view: 'dashboard' },
    { label: 'Trắc nghiệm', icon: 'pi pi-question-circle', view: 'dashboard' },
  ];

  stats = [
    { label: 'Khóa học đăng ký', value: '12', icon: 'pi pi-book', colorClass: 'bg-indigo-50 text-indigo-500' },
    { label: 'Khóa học hoạt động', value: '08', icon: 'pi pi-video', colorClass: 'bg-red-50 text-red-400' },
    { label: 'Khóa học hoàn thành', value: '06', icon: 'pi pi-check-circle', colorClass: 'bg-green-50 text-green-500' },
    { label: 'Tổng số học viên', value: '17', icon: 'pi pi-users', colorClass: 'bg-blue-50 text-blue-500' },
    { label: 'Tổng số khóa học', value: '11', icon: 'pi pi-list', colorClass: 'bg-cyan-50 text-cyan-400' },
    { label: 'Tổng thu nhập', value: '$486', icon: 'pi pi-dollar', colorClass: 'bg-purple-50 text-purple-500' },
  ];

  // Course Stats specifically for the Courses view
  courseStats = [
    { label: 'Active Courses', value: '45', bgColor: 'bg-[#00d084]' },
    { label: 'Pending Courses', value: '21', bgColor: 'bg-[#ff4d6d]' },
    { label: 'Draft Courses', value: '15', bgColor: 'bg-[#6c5ce7]' },
    { label: 'Free Courses', value: '16', bgColor: 'bg-[#00c2ff]' },
    { label: 'Paid Courses', value: '21', bgColor: 'bg-[#a349c2]' },
  ];

  allCourses = [
    { id: 1, title: 'Information About UI/UX Design Degree', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 600, price: 160, rating: 4.5, reviews: 300, status: 'PUBLISHED', image: 'https://images.unsplash.com/photo-1541462608141-ad4d45a446a4?w=100' },
    { id: 2, title: 'Wordpress for Beginners - Master Wordpress Quickly', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 500, price: 180, rating: 4.2, reviews: 430, status: 'PENDING', image: 'https://images.unsplash.com/photo-1614332287897-cdc485fa562d?w=100' },
    { id: 3, title: 'Sketch from A to Z (2024): Become an app designer', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 300, price: 200, rating: 4.7, reviews: 140, status: 'DRAFT', image: 'https://images.unsplash.com/photo-1586717791821-3f44a563dc4c?w=100' },
    { id: 4, title: 'Build Responsive Real World Websites with Crash Course', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 400, price: 220, rating: 4.4, reviews: 260, status: 'PUBLISHED', image: 'https://images.unsplash.com/photo-1547658719-da2b51169166?w=100' },
    { id: 5, title: 'Learn JavaScript and Express to become a Expert', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 700, price: 170, rating: 4.8, reviews: 180, status: 'PUBLISHED', image: 'https://images.unsplash.com/photo-1579468118864-1b9ea3c0db4a?w=100' },
    { id: 6, title: 'Introduction to Python Programming', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 450, price: 150, rating: 4.8, reviews: 180, status: 'PUBLISHED', image: 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=100' },
    { id: 7, title: 'Build Responsive Websites with HTML5 and CSS3', lessons: 11, quizzes: 2, duration: '03:15:00', enrolled: 620, price: 130, rating: 4.9, reviews: 510, status: 'PUBLISHED', image: 'https://images.unsplash.com/photo-1508921334172-b68ed335b3e6?w=100' },
  ];

  earningsData = [
    { month: 'Jan', value: 80 }, { month: 'Feb', value: 100 }, { month: 'Mar', value: 70 },
    { month: 'Apr', value: 110 }, { month: 'May', value: 80 }, { month: 'Jun', value: 90 },
    { month: 'Jul', value: 85 }, { month: 'Aug', value: 85 }, { month: 'Sep', value: 110 },
    { month: 'Oct', value: 30 }, { month: 'Nov', value: 100 }, { month: 'Dec', value: 90 },
  ];

  ngOnInit() {}

  getStatusClass(status: string): string {
    switch(status) {
      case 'PUBLISHED': return 'bg-green-100 text-green-600';
      case 'PENDING': return 'bg-blue-100 text-blue-600';
      case 'DRAFT': return 'bg-purple-100 text-purple-600';
      default: return 'bg-gray-100 text-gray-600';
    }
  }
}
