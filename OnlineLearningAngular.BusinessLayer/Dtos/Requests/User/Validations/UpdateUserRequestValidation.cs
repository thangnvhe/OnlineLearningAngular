using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.User.Validations
{
    public class UpdateUserRequestValidation : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidation()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống.")
                .MaximumLength(100).WithMessage("Họ tên không được vượt quá 100 ký tự.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống.")
                .MinimumLength(3).WithMessage("Tên đăng nhập phải có ít nhất 3 ký tự.")
                .MaximumLength(50).WithMessage("Tên đăng nhập không được vượt quá 50 ký tự.")
                .Matches("^[a-zA-Z0-9._-]+$").WithMessage("Tên đăng nhập chỉ được chứa chữ cái, số và các ký tự . _ -");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Email không đúng định dạng.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches("^\\+?[0-9]{9,15}$").WithMessage("Số điện thoại không đúng định dạng.");

            RuleFor(x => x.Dob)
                .Must(dob => dob <= DateOnly.FromDateTime(DateTime.UtcNow.Date))
                .WithMessage("Ngày sinh không được lớn hơn ngày hiện tại.")
                .Must(dob => dob <= DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-13)))
                .WithMessage("Người dùng phải từ 13 tuổi trở lên.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ không được để trống.")
                .MaximumLength(255).WithMessage("Địa chỉ không được vượt quá 255 ký tự.");

            RuleFor(x => x.Roles)
                .NotNull().WithMessage("Danh sách quyền không được null.");
        }
    }
}
