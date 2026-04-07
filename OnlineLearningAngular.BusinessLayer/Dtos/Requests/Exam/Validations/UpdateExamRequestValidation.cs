using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam.Validations
{
    public class UpdateExamRequestValidation : AbstractValidator<UpdateExamRequest>
    {
        public UpdateExamRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên bài thi không được để trống.")
                .MaximumLength(255).WithMessage("Tên bài thi không được vượt quá 255 ký tự.");

            RuleFor(x => x.ModuleId)
                .GreaterThan(0).WithMessage("Mã module không hợp lệ.");
        }
    }
}
