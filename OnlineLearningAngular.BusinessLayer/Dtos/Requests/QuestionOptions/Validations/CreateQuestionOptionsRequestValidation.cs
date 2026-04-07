using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions.Validations
{
    public class CreateQuestionOptionsRequestValidation : AbstractValidator<CreateQuestionOptionsRequest>
    {
        public CreateQuestionOptionsRequestValidation()
        {
            RuleFor(x => x.OptionName)
                .NotEmpty().WithMessage("Nội dung tùy chọn không được để trống.");

            RuleFor(x => x.QuestionId)
                .GreaterThan(0).WithMessage("Mã câu hỏi không hợp lệ.");
        }
    }
}
