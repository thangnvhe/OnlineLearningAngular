using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail.Validations;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User.Validations;
using OnlineLearningAngular.BusinessLayer.Services.Implementations;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Data.DbInitializer;
using OnlineLearningAngular.DataAccess.Repositories.Implementations;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;

namespace OnlineLearningAngular.API.Configurations
{
    public static class DependencesInjectionConfiguration
    {
        public static void AddDependenceInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserServcie>();
            services.AddScoped<IEmailService, EmailService>();
            
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionOptionsRepository, QuestionOptionsRepository>();
            services.AddScoped<IStudentExamRepository, StudentExamRepository>();
            services.AddScoped<IStudentExamDetailRepository, StudentExamDetailRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();

            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionOptionsService, QuestionOptionsService>();
            services.AddScoped<IStudentExamService, StudentExamService>();
            services.AddScoped<IStudentExamDetailService, StudentExamDetailService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidation>();
            services.AddScoped<IValidator<LoginRequest>, LoginRequestValidation>();
            services.AddScoped<IValidator<ForgotPasswordRequest>, ForgotPasswordRequestValidation>();
            services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordRequestValidation>();
            services.AddScoped<IValidator<ConfirmEmailRequest>, ConfirmEmailRequestValidation>();
            services.AddScoped<IValidator<ChangePasswordRequest>, ChangePasswordRequestValidation>();
            services.AddScoped<IValidator<ResendConfirmationEmail>, ResendConfirmationEmailValidation>();
            services.AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidation>();
            services.AddScoped<IValidator<CreateCourseRequest>, CreateCourseRequestValidation>();
            services.AddScoped<IValidator<UpdateCourseRequest>, UpdateCourseRequestValidation>();
            services.AddScoped<IValidator<CreateModuleRequest>, CreateModuleRequestValidation>();
            services.AddScoped<IValidator<UpdateModuleRequest>, UpdateModuleRequestValidation>();
            services.AddScoped<IValidator<CreateExamRequest>, CreateExamRequestValidation>();
            services.AddScoped<IValidator<UpdateExamRequest>, UpdateExamRequestValidation>();
            services.AddScoped<IValidator<CreateQuestionRequest>, CreateQuestionRequestValidation>();
            services.AddScoped<IValidator<UpdateQuestionRequest>, UpdateQuestionRequestValidation>();
            services.AddScoped<IValidator<CreateQuestionOptionsRequest>, CreateQuestionOptionsRequestValidation>();
            services.AddScoped<IValidator<UpdateQuestionOptionsRequest>, UpdateQuestionOptionsRequestValidation>();
            services.AddScoped<IValidator<CreateStudentExamRequest>, CreateStudentExamRequestValidation>();
            services.AddScoped<IValidator<UpdateStudentExamRequest>, UpdateStudentExamRequestValidation>();
            services.AddScoped<IValidator<CreateLessonRequest>, CreateLessonRequestValidation>();
            services.AddScoped<IValidator<UpdateLessonRequest>, UpdateLessonRequestValidation>();
            services.AddScoped<IValidator<CreateStudentExamDetailRequest>, CreateStudentExamDetailRequestValidation>();
            services.AddScoped<IValidator<UpdateStudentExamDetailRequest>, UpdateStudentExamDetailRequestValidation>();
            services.AddScoped<IValidator<UpdateRolePermissionsRequest>, UpdateRolePermissionsRequestValidation>();
            
        }
    }
}
