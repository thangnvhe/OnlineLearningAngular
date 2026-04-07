using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace OnlineLearningAngular.API.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger; // Log ra console của Visual Studio
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, ILogService logService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var source = context.Request.Path.Value;
                await logService.LogError(source ?? "Unknown", "GlobalException", ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // TẠI ĐÂY: Sử dụng đúng cấu trúc APIResponse của bạn
                // Giả sử APIResponse của bạn có thể nhận data null
                var response = APIResponse<object>.Builder()
                    .WithSuccess(false)
                    .WithStatusCode(HttpStatusCode.InternalServerError)
                    .WithMessage(_env.IsDevelopment() ? ex.Message : "Đã có lỗi hệ thống xảy ra!")
                    // Nếu APIResponse của bạn có chỗ lưu StackTrace thì cho vào, không thì thôi
                    .Build();

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
