using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.QuestionOptions;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionsController : BaseController
    {
        private readonly IQuestionOptionsService _questionOptionsService;

        public QuestionOptionsController(IQuestionOptionsService questionOptionsService)
        {
            _questionOptionsService = questionOptionsService;
        }

        [HttpGet("question/{questionId}")]
        [HasPermission(Permissions.QuestionOptions.View)]
        public async Task<IActionResult> GetQuestionOptionsByQuestionIdAsync(int questionId)
        {
            var result = await _questionOptionsService.GetQuestionOptionsByQuestionIdAsync(questionId);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionOptionsByIdAsync(int id)
        {
            var result = await _questionOptionsService.GetQuestionOptionsByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.QuestionOptions.Create)]
        public async Task<IActionResult> CreateQuestionOptionsAsync([FromBody] CreateQuestionOptionsRequest request)
        {
            var result = await _questionOptionsService.CreateQuestionOptionsAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.QuestionOptions.Edit)]
        public async Task<IActionResult> UpdateQuestionOptionsAsync(int id, [FromBody] UpdateQuestionOptionsRequest request)
        {
            var result = await _questionOptionsService.UpdateQuestionOptionsAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.QuestionOptions.Delete)]
        public async Task<IActionResult> DeleteQuestionOptionsAsync(int id)
        {
            var result = await _questionOptionsService.DeleteQuestionOptionsAsync(id);
            return HandleResult(result);
        }
    }
}
