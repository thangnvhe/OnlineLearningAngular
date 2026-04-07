using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.StudentExamDetail;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamDetailController : BaseController
    {
        private readonly IStudentExamDetailService _studentExamDetailService;

        public StudentExamDetailController(IStudentExamDetailService studentExamDetailService)
        {
            _studentExamDetailService = studentExamDetailService;
        }

        [HttpGet("studentexam/{studentExamId}")]
        [HasPermission(Permissions.StudentExamDetails.View)]
        public async Task<IActionResult> GetStudentExamDetailsByStudentExamIdAsync(int studentExamId)
        {
            var result = await _studentExamDetailService.GetStudentExamDetailsByStudentExamIdAsync(studentExamId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.StudentExamDetails.View)]
        public async Task<IActionResult> GetStudentExamDetailByIdAsync(int id)
        {
            var result = await _studentExamDetailService.GetStudentExamDetailByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.StudentExamDetails.Create)]
        public async Task<IActionResult> CreateStudentExamDetailAsync([FromBody] CreateStudentExamDetailRequest request)
        {
            var result = await _studentExamDetailService.CreateStudentExamDetailAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.StudentExamDetails.Edit)]
        public async Task<IActionResult> UpdateStudentExamDetailAsync(int id, [FromBody] UpdateStudentExamDetailRequest request)
        {
            var result = await _studentExamDetailService.UpdateStudentExamDetailAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.StudentExamDetails.Delete)]
        public async Task<IActionResult> DeleteStudentExamDetailAsync(int id)
        {
            var result = await _studentExamDetailService.DeleteStudentExamDetailAsync(id);
            return HandleResult(result);
        }
    }
}
