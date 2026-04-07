using FluentValidation;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths.Validations
{
    public class ConfirmEmailRequestValidation : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidation()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId không được để trống.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token không được để trống.");
        }
    }
}
