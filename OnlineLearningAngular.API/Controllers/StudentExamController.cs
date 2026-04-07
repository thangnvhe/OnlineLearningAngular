using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExam;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamController : BaseController
    {
        private readonly IStudentExamService _studentExamService;

        public StudentExamController(IStudentExamService studentExamService)
        {
            _studentExamService = studentExamService;
        }

        [HttpGet("exam/{examId}/paged")]
        [HasPermission(Permissions.StudentExams.View)]
        public async Task<IActionResult> GetPagedStudentExamByExamIdAsync(int examId, [FromQuery] PagingFilterBase filters)
        {
            var result = await _studentExamService.GetPagedStudentExamByExamIdAsync(examId, filters);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentExamByIdAsync(int id)
        {
            var result = await _studentExamService.GetStudentExamByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.StudentExams.Create)]
        public async Task<IActionResult> CreateStudentExamAsync([FromBody] CreateStudentExamRequest request)
        {
            var result = await _studentExamService.CreateStudentExamAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.StudentExams.Edit)]
        public async Task<IActionResult> UpdateStudentExamAsync(int id, [FromBody] UpdateStudentExamRequest request)
        {
            var result = await _studentExamService.UpdateStudentExamAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.StudentExams.Delete)]
        public async Task<IActionResult> DeleteStudentExamAsync(int id)
        {
            var result = await _studentExamService.DeleteStudentExamAsync(id);
            return HandleResult(result);
        }
    }
}
