using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail.Validations
{
    public class UpdateStudentExamDetailRequestValidation : AbstractValidator<UpdateStudentExamDetailRequest>
    {
        public UpdateStudentExamDetailRequestValidation()
        {
            RuleFor(x => x.StudentExamId)
                .GreaterThan(0).WithMessage("Mã bài làm không hợp lệ.");

            RuleFor(x => x.QuestionId)
                .GreaterThan(0).WithMessage("Mã câu hỏi không hợp lệ.");
        }
    }
}
