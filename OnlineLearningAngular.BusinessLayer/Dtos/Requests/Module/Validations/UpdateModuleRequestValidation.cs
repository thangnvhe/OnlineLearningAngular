using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module.Validations
{
    public class UpdateModuleRequestValidation : AbstractValidator<UpdateModuleRequest>
    {
        public UpdateModuleRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên module không được để trống.")
                .MaximumLength(255).WithMessage("Tên module không được vượt quá 255 ký tự.");

            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("Mã khóa học không hợp lệ.");
        }
    }
}
