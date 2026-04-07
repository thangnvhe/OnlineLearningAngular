import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Course } from '../../../core/models/course.model';
import { CourseStatus, CourseType } from '../../../core/models/base/enum.model';

@Component({
  selector: 'app-view-courses',
  templateUrl: './view-courses.html',
  styleUrl: './view-courses.css',
  changeDetection: ChangeDetectionStrategy.Default,
  imports: [CommonModule, FormsModule],
})
export class ViewCourses {
  searchQuery = signal('');
  selectedCategories = signal<number[]>([]);
  selectedInstructors = signal<number[]>([]);
  sortBy = signal('new');
  isSidebarOpen = signal(false);

  // Enum Options for UI
  categoryOptions = [
    { id: CourseType.Programming, label: 'Lập trình' },
    { id: CourseType.Design, label: 'Thiết kế' },
    { id: CourseType.Marketing, label: 'Marketing' },
    { id: CourseType.Photography, label: 'Nhiếp ảnh' },
  ];

  instructorOptions = [
    { id: 101, name: 'Brenda Slaton' },
    { id: 102, name: 'Ana Reyes' },
    { id: 103, name: 'Andrew Pirtle' },
    { id: 104, name: 'Nicole Brown' },
  ];

  // Updated Mock Data
  courses: Course[] = [
    {
      id: 1,
      name: 'Information About UI/UX Design Degree',
      imageUrl: 'https://images.unsplash.com/photo-1586717791821-3f44a563eb4c?w=400',
      price: 120,
      authorId: 101,
      authorName: 'Brenda Slaton',
      courseType: CourseType.Design,
      courseStatus: CourseStatus.Published,
      createAt: '2024-01-01',
      updateAt: '2024-01-01',
      rating: 4.9,
      reviews: 200,
    },
    {
      id: 2,
      name: 'Wordpress for Beginners - Master Wordpress Quickly',
      imageUrl: 'https://images.unsplash.com/photo-1614332287897-cdc485fa562d?w=400',
      price: 140,
      authorId: 102,
      authorName: 'Ana Reyes',
      courseType: CourseType.Programming,
      courseStatus: CourseStatus.Published,
      createAt: '2024-01-05',
      updateAt: '2024-01-05',
      rating: 4.4,
      reviews: 160,
    },
    {
      id: 3,
      name: 'Sketch from A to Z (2024): Become an app designer',
      imageUrl: 'https://images.unsplash.com/photo-1541462608141-ad516aeb280a?w=400',
      price: 140,
      authorId: 103,
      authorName: 'Andrew Pirtle',
      courseType: CourseType.Design,
      courseStatus: CourseStatus.Published,
      createAt: '2024-02-10',
      updateAt: '2024-02-10',
      rating: 4.4,
      reviews: 160,
    },
    {
      id: 4,
      name: 'Complete Python Bootcamp: Go from zero to hero',
      imageUrl: 'https://images.unsplash.com/photo-1526374965328-7f61d4dc18c5?w=400',
      price: 180,
      authorId: 104,
      authorName: 'Nicole Brown',
      courseType: CourseType.Programming,
      courseStatus: CourseStatus.Published,
      createAt: '2024-03-01',
      updateAt: '2024-03-01',
      rating: 4.8,
      reviews: 540,
    },
    {
      id: 5,
      name: 'Photography Masterclass: A Complete Guide',
      imageUrl: 'https://images.unsplash.com/photo-1452587925148-ce544e77e70d?w=400',
      price: 90,
      authorId: 104,
      authorName: 'Nicole Brown',
      courseType: CourseType.Photography,
      courseStatus: CourseStatus.Published,
      createAt: '2024-03-15',
      updateAt: '2024-03-15',
      rating: 4.7,
      reviews: 89,
    },
  ];

  filteredCourses = computed(() => {
    let result = this.courses.filter((c) => {
      const matchesSearch = c.name.toLowerCase().includes(this.searchQuery().toLowerCase());
      const matchesCat =
        this.selectedCategories().length === 0 || this.selectedCategories().includes(c.courseType);
      const matchesIns =
        this.selectedInstructors().length === 0 || this.selectedInstructors().includes(c.authorId);
      return matchesSearch && matchesCat && matchesIns;
    });

    if (this.sortBy() === 'low') result.sort((a, b) => a.price - b.price);
    if (this.sortBy() === 'high') result.sort((a, b) => b.price - a.price);
    if (this.sortBy() === 'new')
      result.sort((a, b) => new Date(b.createAt).getTime() - new Date(a.createAt).getTime());

    return result;
  });

  getTypeName(type: CourseType): string {
    return CourseType[type];
  }

  toggleCategory(typeId: number) {
    const current = this.selectedCategories();
    this.selectedCategories.set(
      current.includes(typeId) ? current.filter((id) => id !== typeId) : [...current, typeId],
    );
  }

  toggleInstructor(authorId: number) {
    const current = this.selectedInstructors();
    this.selectedInstructors.set(
      current.includes(authorId) ? current.filter((id) => id !== authorId) : [...current, authorId],
    );
  }

  clearFilters() {
    this.selectedCategories.set([]);
    this.selectedInstructors.set([]);
    this.searchQuery.set('');
  }

  toggleSidebar() {
    this.isSidebarOpen.update((v) => !v);
  }
}
