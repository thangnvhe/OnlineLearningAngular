using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineLearningAngular.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAngular.BusinessLayer.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null) return;

            // 1. Lấy UserId từ JWT Claims
            var userIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return;

            using var scope = _serviceScopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // 2. Lấy User từ DB
            var user = await userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return;

            // 3. Lấy danh sách Role của User
            var userRoles = await userManager.GetRolesAsync(user);

            // 4. Kiểm tra quyền trong Claims của từng Role (Đây là cách Identity lưu Permission)
            foreach (var roleName in userRoles)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var roleClaims = await roleManager.GetClaimsAsync(role);
                    if (roleClaims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission))
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }
            }
        }
    }
}
