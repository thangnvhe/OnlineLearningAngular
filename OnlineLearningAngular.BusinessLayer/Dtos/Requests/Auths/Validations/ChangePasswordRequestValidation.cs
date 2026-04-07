using FluentValidation;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths.Validations
{
    public class ChangePasswordRequestValidation : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidation()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mật khẩu hiện tại không được để trống.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Mật khẩu mới không được để trống.")
                .MinimumLength(8).WithMessage("Mật khẩu mới phải có ít nhất 8 ký tự.")
                .Matches("[A-Z]").WithMessage("Mật khẩu mới phải chứa ít nhất 1 chữ hoa.")
                .Matches("[a-z]").WithMessage("Mật khẩu mới phải chứa ít nhất 1 chữ thường.")
                .Matches("[0-9]").WithMessage("Mật khẩu mới phải chứa ít nhất 1 chữ số.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Mật khẩu mới phải chứa ít nhất 1 ký tự đặc biệt.")
                .NotEqual(x => x.CurrentPassword).WithMessage("Mật khẩu mới phải khác mật khẩu hiện tại.");
        }
    }
}
