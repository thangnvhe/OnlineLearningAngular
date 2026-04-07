using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course.Validations
{
    public class UpdateCourseRequestValidation : AbstractValidator<UpdateCourseRequest>
    {
        public UpdateCourseRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên khóa học không được để trống.")
                .MaximumLength(255).WithMessage("Tên khóa học không được vượt quá 255 ký tự.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Giá khóa học không được nhỏ hơn 0.");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Mã tác giả không hợp lệ.");
        }
    }
}
