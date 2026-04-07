using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.BusinessLayer;
using System.Net;

namespace OnlineLearningAngular.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(ServiceResult<T> result)
        {
            if (!result.IsSuccess)
            {
                var response = APIResponse<T>.Builder()
                    .WithSuccess(false)
                    .WithStatusCode(HttpStatusCode.BadRequest)
                    .WithMessage(result.ErrorMessage!)
                    .Build();
                return BadRequest(response);
            }

            var successResponse = APIResponse<T>.Builder()
                .WithSuccess(true)
                .WithStatusCode(HttpStatusCode.OK)
                .WithResult(result.Data!)
                .Build();
            return Ok(successResponse);
        }

        protected IActionResult Handle<T>(ServiceResult<T> result) => HandleResult(result);
    }
}
