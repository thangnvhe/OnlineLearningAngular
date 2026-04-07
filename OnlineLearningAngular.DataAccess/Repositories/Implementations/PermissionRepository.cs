using Microsoft.AspNetCore.Identity;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace OnlineLearningAngular.DataAccess.Repositories.Implementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public PermissionRepository(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<string>> GetAllPermissionsAsync()
        {
            return await Task.FromResult(Permissions.GetAllPermissions());
        }

        public async Task<List<string>> GetPermissionsByRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return new List<string>();

            var claims = await _roleManager.GetClaimsAsync(role);
            return claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
        }

        public async Task<bool> UpdatePermissionsForRoleAsync(string roleName, List<string> permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return false;

            var existingClaims = await _roleManager.GetClaimsAsync(role);
            var permissionClaims = existingClaims.Where(c => c.Type == "Permission").ToList();

            foreach (var claim in permissionClaims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            foreach (var permission in permissions)
            {
                await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }

            return true;
        }
    }
}
