using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;

namespace OnlineLearningAngular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : BaseController
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagedLogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _logService.GetPagedLogsAsync(null, pageNumber, pageSize);
            return Handle(result);
        }
    }
}
