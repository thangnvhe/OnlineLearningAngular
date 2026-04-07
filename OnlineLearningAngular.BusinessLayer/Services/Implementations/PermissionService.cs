using FluentValidation;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IValidator<UpdateRolePermissionsRequest> _validator;

        public PermissionService(
            IPermissionRepository permissionRepository, 
            IValidator<UpdateRolePermissionsRequest> validator)
        {
            _permissionRepository = permissionRepository;
            _validator = validator;
        }

        public async Task<ServiceResult<List<string>>> GetAllPermissionsAsync()
        {
            var permissions = await _permissionRepository.GetAllPermissionsAsync();
            return ServiceResult<List<string>>.Success(permissions);
        }

        public async Task<ServiceResult<List<string>>> GetPermissionsByRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return ServiceResult<List<string>>.Failure("Tên Role không hợp lệ.");
            }

            var permissions = await _permissionRepository.GetPermissionsByRoleAsync(roleName);
            return ServiceResult<List<string>>.Success(permissions);
        }

        public async Task<ServiceResult<bool>> UpdatePermissionsForRoleAsync(string roleName, UpdateRolePermissionsRequest request)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return ServiceResult<bool>.Failure("Tên Role không hợp lệ.");
            }

            if (request == null)
            {
                return ServiceResult<bool>.Failure("Dữ liệu không hợp lệ.");
            }

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var result = await _permissionRepository.UpdatePermissionsForRoleAsync(roleName, request.Permissions);
            if (!result)
            {
                return ServiceResult<bool>.Failure("Lỗi cập nhật hoặc không tìm thấy Role.");
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
