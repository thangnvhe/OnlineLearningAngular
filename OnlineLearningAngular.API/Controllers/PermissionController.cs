using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Permission;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using System.Threading.Tasks;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize(Roles = Utilizer.Role_SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPermissionsAsync()
        {
            var result = await _permissionService.GetAllPermissionsAsync();
            return HandleResult(result);
        }

        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetPermissionsByRoleAsync(string roleName)
        {
            var result = await _permissionService.GetPermissionsByRoleAsync(roleName);
            return HandleResult(result);
        }

        [HttpPut("role/{roleName}")]
        public async Task<IActionResult> UpdatePermissionsForRoleAsync(string roleName, [FromBody] UpdateRolePermissionsRequest request)
        {
            var result = await _permissionService.UpdatePermissionsForRoleAsync(roleName, request);
            return HandleResult(result);
        }
    }
}
