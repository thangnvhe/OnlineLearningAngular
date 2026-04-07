using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Module;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet("course/{courseId}")]
        [HasPermission(Permissions.Modules.View)]
        public async Task<IActionResult> GetModulesByCourseIdAsync(int courseId)
        {
            var result = await _moduleService.GetModulesByCourseIdAsync(courseId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModuleByIdAsync(int id)
        {
            var result = await _moduleService.GetModuleByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Modules.Create)]
        public async Task<IActionResult> CreateModuleAsync([FromBody] CreateModuleRequest request)
        {
            var result = await _moduleService.CreateModuleAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Modules.Edit)]
        public async Task<IActionResult> UpdateModuleAsync(int id, [FromBody] UpdateModuleRequest request)
        {
            var result = await _moduleService.UpdateModuleAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Modules.Delete)]
        public async Task<IActionResult> DeleteModuleAsync(int id)
        {
            var result = await _moduleService.DeleteModuleAsync(id);
            return HandleResult(result);
        }
    }
}
