using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Question;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : BaseController
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("exam/{examId}")]
        [HasPermission(Permissions.Questions.View)]
        public async Task<IActionResult> GetQuestionsByExamIdAsync(int examId)
        {
            var result = await _questionService.GetQuestionsByExamIdAsync(examId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionByIdAsync(int id)
        {
            var result = await _questionService.GetQuestionByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Questions.Create)]
        public async Task<IActionResult> CreateQuestionAsync([FromBody] CreateQuestionRequest request)
        {
            var result = await _questionService.CreateQuestionAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Questions.Edit)]
        public async Task<IActionResult> UpdateQuestionAsync(int id, [FromBody] UpdateQuestionRequest request)
        {
            var result = await _questionService.UpdateQuestionAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Questions.Delete)]
        public async Task<IActionResult> DeleteQuestionAsync(int id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            return HandleResult(result);
        }
    }
}
