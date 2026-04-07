using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson.Validations
{
    public class UpdateLessonRequestValidation : AbstractValidator<UpdateLessonRequest>
    {
        public UpdateLessonRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên bài học không được để trống.")
                .MaximumLength(255).WithMessage("Tên bài học không được vượt quá 255 ký tự.");

            RuleFor(x => x.ModuleId)
                .GreaterThan(0).When(x => x.ModuleId.HasValue).WithMessage("Mã module không hợp lệ.");
        }
    }
}
