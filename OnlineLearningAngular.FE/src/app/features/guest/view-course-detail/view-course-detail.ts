import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, signal } from '@angular/core';

@Component({
  selector: 'app-view-course-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-course-detail.html',
  styleUrl: './view-course-detail.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ViewCourseDetail {
  curriculum = signal([
    {
      id: 1,
      title: 'Bắt đầu',
      isOpen: true,
      lessons: [
        { id: 101, title: 'Giới thiệu về khóa học User Experience', duration: '02:53' },
        { id: 102, title: 'Bài tập: Thử thách thiết kế đầu tiên của bạn', duration: '02:53' },
        { id: 103, title: 'Cách giải quyết bài tập trước đó', duration: '02:53' },
        { id: 104, title: 'Tại sao Smart Objects lại tuyệt vời', duration: '02:53' },
        { id: 105, title: 'Cách sử dụng Text Layers hiệu quả', duration: '02:53' },
      ],
    },
    { id: 2, title: 'The Brief', isOpen: false, lessons: [] },
    { id: 3, title: 'Wireframing Low Fidelity', isOpen: false, lessons: [] },
    { id: 4, title: 'Giới thiệu Type, Color & Icon', isOpen: false, lessons: [] },
  ]);

  toggleSection(id: number) {
    this.curriculum.update((sections) =>
      sections.map((s) => (s.id === id ? { ...s, isOpen: !s.isOpen } : s)),
    );
  }
}
