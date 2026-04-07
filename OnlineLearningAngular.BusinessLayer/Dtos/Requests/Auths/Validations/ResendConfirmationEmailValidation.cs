using FluentValidation;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths.Validations
{
    public class ResendConfirmationEmailValidation : AbstractValidator<ResendConfirmationEmail>
    {
        public ResendConfirmationEmailValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống.")
                .MinimumLength(3).WithMessage("Tên đăng nhập phải có ít nhất 3 ký tự.")
                .MaximumLength(50).WithMessage("Tên đăng nhập không được vượt quá 50 ký tự.");
        }
    }
}
