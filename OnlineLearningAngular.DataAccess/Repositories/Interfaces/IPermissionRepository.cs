using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearningAngular.DataAccess.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        Task<List<string>> GetAllPermissionsAsync();
        Task<List<string>> GetPermissionsByRoleAsync(string roleName);
        Task<bool> UpdatePermissionsForRoleAsync(string roleName, List<string> permissions);
    }
}
