using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Lesson;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet("module/{moduleId}")]
        [HasPermission(Permissions.Lessons.View)]
        public async Task<IActionResult> GetLessonsByModuleIdAsync(int moduleId)
        {
            var result = await _lessonService.GetLessonsByModuleIdAsync(moduleId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonByIdAsync(int id)
        {
            var result = await _lessonService.GetLessonByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Lessons.Create)]
        public async Task<IActionResult> CreateLessonAsync([FromBody] CreateLessonRequest request)
        {
            var result = await _lessonService.CreateLessonAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Lessons.Edit)]
        public async Task<IActionResult> UpdateLessonAsync(int id, [FromBody] UpdateLessonRequest request)
        {
            var result = await _lessonService.UpdateLessonAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Lessons.Delete)]
        public async Task<IActionResult> DeleteLessonAsync(int id)
        {
            var result = await _lessonService.DeleteLessonAsync(id);
            return HandleResult(result);
        }
    }
}
