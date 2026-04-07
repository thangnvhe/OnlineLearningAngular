using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam.Validations
{
    public class CreateStudentExamRequestValidation : AbstractValidator<CreateStudentExamRequest>
    {
        public CreateStudentExamRequestValidation()
        {
            RuleFor(x => x.StudentId)
                .GreaterThan(0).WithMessage("Mã sinh viên không hợp lệ.");

            RuleFor(x => x.ExamId)
                .GreaterThan(0).WithMessage("Mã bài thi không hợp lệ.");

            RuleFor(x => x.Score)
                .InclusiveBetween(0, 100).WithMessage("Điểm số phải từ 0 đến 100.");
        }
    }
}
