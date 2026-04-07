using FluentValidation;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths.Validations
{
    public class ForgotPasswordRequestValidation : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Email không đúng định dạng.");
        }
    }
}
