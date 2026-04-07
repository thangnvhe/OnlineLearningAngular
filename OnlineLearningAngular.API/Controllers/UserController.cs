using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAngular.API.Controllers.Base;
using OnlineLearningAngular.API.Helpers;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            return HandleResult(result);
        }

        [HttpGet("Paged")]
        [HasPermission(Permissions.Users.View)]
        public async Task<IActionResult> GetPagedUsersAsync([FromQuery] PagingFilterBase filters)
        {
            var result = await _userService.GetPagedUsersAsync(filters);
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        [HasPermission(Permissions.Users.View)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPost]
        [HasPermission(Permissions.Users.Create)]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterRequest request)
        {
            var result = await _userService.CreateUserAsync(request);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        [HasPermission(Permissions.Users.Edit)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserRequest request)
        {
            var result = await _userService.UpdateUserAsync(id, request);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        [HasPermission(Permissions.Users.Delete)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return HandleResult(result);
        }
    }
}
