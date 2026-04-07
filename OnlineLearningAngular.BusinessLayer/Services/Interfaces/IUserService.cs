using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.User;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.DataAccess.Common;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<List<UserResponse>>> GetAllUsersAsync();
        Task<ServiceResult<PagedResult<UserResponse>>> GetPagedUsersAsync(PagingFilterBase filters);
        Task<ServiceResult<UserResponse?>> GetUserByIdAsync(int id);
        Task<ServiceResult<UserResponse?>> CreateUserAsync(RegisterRequest request);
        Task<ServiceResult<UserResponse?>> UpdateUserAsync(int id, UpdateUserRequest request);
        Task<ServiceResult<bool>> DeleteUserAsync(int id);
    }
}
