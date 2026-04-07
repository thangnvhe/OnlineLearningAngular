using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Exam;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("module/{moduleId}")]
        [HasPermission(Permissions.Exams.View)]
        public async Task<IActionResult> GetExamsByModuleIdAsync(int moduleId)
        {
            var result = await _examService.GetExamsByModuleIdAsync(moduleId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExamByIdAsync(int id)
        {
            var result = await _examService.GetExamByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Exams.Create)]
        public async Task<IActionResult> CreateExamAsync([FromBody] CreateExamRequest request)
        {
            var result = await _examService.CreateExamAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Exams.Edit)]
        public async Task<IActionResult> UpdateExamAsync(int id, [FromBody] UpdateExamRequest request)
        {
            var result = await _examService.UpdateExamAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Exams.Delete)]
        public async Task<IActionResult> DeleteExamAsync(int id)
        {
            var result = await _examService.DeleteExamAsync(id);
            return HandleResult(result);
        }
    }
}
