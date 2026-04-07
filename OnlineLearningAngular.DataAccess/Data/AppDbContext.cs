using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Enums;

namespace OnlineLearningAngular.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOptions> QuestionOptions { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentExamDetail> StudentExamDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.ImageUrl).HasMaxLength(500);
                entity.Property(x => x.Price).HasPrecision(18, 2);
                entity.Property(x => x.CourseStatus).HasDefaultValue(CourseStatus.Draft);   
                entity.HasOne(x => x.Author)
                    .WithMany(x => x.AuthorCourses)
                    .HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(x => new { x.StudentId, x.CourseId });

                entity.HasOne(x => x.Student)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Course)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);
                entity.Property(x => x.ModuleStatus).HasDefaultValue(ModuleStatus.Draft);
                entity.HasOne(x => x.Course)
                    .WithMany(x => x.Modules)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);

                entity.HasOne(x => x.Module)
                    .WithMany(x => x.Lessons)
                    .HasForeignKey(x => x.ModuleId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(x => x.Name).IsRequired().HasMaxLength(200);
                entity.Property(x => x.Description).HasMaxLength(1000);

                entity.HasOne(x => x.Module)
                    .WithMany(x => x.Exams)
                    .HasForeignKey(x => x.ModuleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(x => x.QuestionName).IsRequired().HasMaxLength(1000);

                entity.Property<int>("ExamId");
                entity.HasOne<Exam>()
                    .WithMany(x => x.Questions)
                    .HasForeignKey("ExamId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<QuestionOptions>(entity =>
            {
                entity.Property(x => x.OptionName).IsRequired().HasMaxLength(500);

                entity.HasOne(x => x.Question)
                    .WithMany(x => x.QuestionOptions)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentExam>(entity =>
            {
                entity.HasOne(x => x.Student)
                    .WithMany(x => x.StudentExams)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Exam)
                    .WithMany()
                    .HasForeignKey(x => x.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentExamDetail>(entity =>
            {
                entity.HasOne(x => x.StudentExam)
                    .WithMany(x => x.ExamDetails)
                    .HasForeignKey(x => x.StudentExamId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Question)
                    .WithMany()
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.SelectedOption)
                    .WithMany()
                    .HasForeignKey(x => x.SelectedOptionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(x => x.Progress)
                    .HasPrecision(5, 2)
                    .HasDefaultValue(0);
            });
        }
    }
}
