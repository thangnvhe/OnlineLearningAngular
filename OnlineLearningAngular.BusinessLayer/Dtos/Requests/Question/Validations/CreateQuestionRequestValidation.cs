using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question.Validations
{
    public class CreateQuestionRequestValidation : AbstractValidator<CreateQuestionRequest>
    {
        public CreateQuestionRequestValidation()
        {
            RuleFor(x => x.QuestionName)
                .NotEmpty().WithMessage("Nội dung câu hỏi không được để trống.")
                .MaximumLength(1000).WithMessage("Nội dung câu hỏi không được vượt quá 1000 ký tự.");
        }
    }
}
