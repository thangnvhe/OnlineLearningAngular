using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Course;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var result = await _courseService.GetAllCoursesAsync();
            return HandleResult(result);
        }

        [HttpGet("Paged")]
        public async Task<IActionResult> GetPagedCoursesAsync([FromQuery] PagingFilterBase filters)
        {
            var result = await _courseService.GetPagedCoursesAsync(filters);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseByIdAsync(int id)
        {
            var result = await _courseService.GetCourseByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Courses.Create)]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseRequest request)
        {
            var result = await _courseService.CreateCourseAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Courses.Edit)]
        public async Task<IActionResult> UpdateCourseAsync(int id, [FromBody] UpdateCourseRequest request)
        {
            var result = await _courseService.UpdateCourseAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Courses.Delete)]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            return HandleResult(result);
        }
    }
}
