using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission;
using OnlineLearningAngular.DataAccess.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<ServiceResult<List<string>>> GetAllPermissionsAsync();
        Task<ServiceResult<List<string>>> GetPermissionsByRoleAsync(string roleName);
        Task<ServiceResult<bool>> UpdatePermissionsForRoleAsync(string roleName, UpdateRolePermissionsRequest request);
    }
}
