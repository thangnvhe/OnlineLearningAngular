import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { MainLayout } from './layout/main-layout/main-layout';
import { Home } from './features/guest/home/home';
import { TeacherLayout } from './layout/teacher-layout/teacher-layout';
import { StudentLayout } from './layout/student-layout/student-layout';
import { AdminLayout } from './layout/admin-layout/admin-layout';

export const routes: Routes = [
  // 1. Group các trang cần ĐĂNG NHẬP (AuthGuard) + Dùng chung Layout
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        loadComponent: () => import('./features/guest/home/home').then((m) => m.Home),
      },
      {
        path: 'courses/:id',
        loadComponent: () =>
          import('./features/guest/view-course-detail/view-course-detail').then(
            (m) => m.ViewCourseDetail,
          ),
      },
      {
        path: 'courses',
        loadComponent: () =>
          import('./features/guest/view-courses/view-courses').then((m) => m.ViewCourses),
      },
      {
        path: 'login',
        loadComponent: () => import('./features/auth/login/login').then((m) => m.Login),
      },
      {
        path: 'register',
        loadComponent: () => import('./features/auth/register/register').then((m) => m.Register),
      },
      {
        path: 'forgot-password',
        loadComponent: () =>
          import('./features/auth/forgot-password/forgot-password').then((m) => m.ForgotPassword),
      },
      {
        path: 'resend-confirm-email',
        loadComponent: () =>
          import('./features/auth/resend-confirm-email/resend-confirm-email').then(
            (m) => m.ResendConfirmEmail,
          ),
      },
      {
        path: 'reset-password',
        loadComponent: () =>
          import('./features/auth/reset-password/reset-password').then((m) => m.ResetPassword),
      },

      // 3. Error Pages
      {
        path: 'unauthorized',
        loadComponent: () =>
          import('./features/auth/unauthorized/unauthorized').then((m) => m.Unauthorized),
      },
    ],
  },
  {
    path: 'teacher',
    component: TeacherLayout, // Layout bọc ngoài
    children: [
      {
        path: 'dashboard',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/teacher/dashboard/dashboard').then((m) => m.Dashboard),
      },
      {
        path: 'courses',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/view-courses/view-courses').then((m) => m.ViewCourses),
      },
      {
        path: 'courses/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/create-course/create-course').then((m) => m.CreateCourse),
      },
      {
        path: 'courses/edit/:id',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/update-course/update-course').then((m) => m.UpdateCourse),
      },
      {
        path: 'courses/:courseId/modules',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/modules/view-module/view-module').then((m) => m.ViewModule),
      },
      {
        path: 'courses/:courseId/modules/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/modules/create-module/create-module').then((m) => m.CreateModule),
      },
      {
        path: 'courses/:courseId/modules/edit/:moduleId',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/modules/update-module/update-module').then((m) => m.UpdateModule),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/lessons',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/lessons/view-lesson/view-lesson').then((m) => m.ViewLesson),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/lessons/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/lessons/create-lesson/create-lesson').then((m) => m.CreateLesson),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/lessons/edit/:lessonId',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/lessons/update-lesson/update-lesson').then((m) => m.UpdateLesson),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/exams',
        canActivate: [authGuard],
        loadComponent: () => import('./features/exams/view-exam/view-exam').then((m) => m.ViewExam),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/exams/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/exams/create-exam/create-exam').then((m) => m.CreateExam),
      },
      {
        path: 'courses/:courseId/modules/:moduleId/exams/edit/:examId',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/exams/update-exam/update-exam').then((m) => m.UpdateExam),
      },
      {
        path: 'courses/:courseId/students',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/teacher/manage-students/manage-students').then(
            (m) => m.ManageStudents,
          ),
      },
    ],
  },
  {
    path: 'student',
    component: StudentLayout, // Layout bọc ngoài
    children: [
      {
        path: 'dashboard',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/student/dashboard/dashboard').then((m) => m.Dashboard),
      },
      {
        path: 'my-courses',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/student/my-courses/my-courses').then((m) => m.MyCourses),
      },
      {
        path: 'checkout-course/:id',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/student/purchase-course/purchase-course').then(
            (m) => m.PurchaseCourse,
          ),
      },
      {
        path: 'courses/:courseId/module/:moduleId/lesson/:lessonId',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/lessons/view-lesson/view-lesson').then((m) => m.ViewLesson),
      },
      {
        path: 'courses/:courseId/module/:moduleId/exam/:examId',
        canActivate: [authGuard],
        loadComponent: () => import('./features/exams/view-exam/view-exam').then((m) => m.ViewExam),
      },
    ],
  },
  {
    path: 'admin',
    component: AdminLayout, // Layout bọc ngoài
    children: [
      {
        path: 'dashboard',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/dashboard/dashboard').then((m) => m.Dashboard),
      },
      {
        path: 'manage-users',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/manage-users/view-users/view-users').then((m) => m.ViewUsers),
      },
      {
        path: 'manage-users/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/manage-users/create-user/create-user').then((m) => m.CreateUser),
      },
      {
        path: 'manage-users/edit/:id',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/manage-users/update-user/update-user').then((m) => m.UpdateUser),
      },
      {
        path: 'manage-courses',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/view-courses/view-courses').then((m) => m.ViewCourses),
      },
      {
        path: 'manage-courses/create',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/create-course/create-course').then((m) => m.CreateCourse),
      },
      {
        path: 'manage-courses/edit/:id',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/courses/update-course/update-course').then((m) => m.UpdateCourse),
      },
      {
        path: 'view-logs',
        canActivate: [authGuard],
        loadComponent: () => import('./features/admin/view-log/view-log').then((m) => m.ViewLog),
      },
      {
        path: 'manage-permissions',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/view-premissions/view-premissions').then(
            (m) => m.ViewPremissions,
          ),
      },
      {
        path: 'manage-permissions/edit/:id',
        canActivate: [authGuard],
        loadComponent: () =>
          import('./features/admin/update-premission/update-premission').then(
            (m) => m.UpdatePremission,
          ),
      },
    ],
  },

  {
    path: '**', // Wildcard route cho 404
    redirectTo: '',
  },
];
