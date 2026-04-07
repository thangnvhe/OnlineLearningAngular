using FluentValidation;

namespace OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission.Validations
{
    public class UpdateRolePermissionsRequestValidation : AbstractValidator<UpdateRolePermissionsRequest>
    {
        public UpdateRolePermissionsRequestValidation()
        {
            RuleFor(x => x.Permissions)
                .NotNull().WithMessage("Danh sách quyền (Permissions) không được bỏ trống.");
        }
    }
}
