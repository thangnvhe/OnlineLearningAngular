using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;

namespace OnlineLearningAngular.BusinessLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResult<AuthResponse>> LoginAsync(LoginRequest request);
        Task<ServiceResult<bool>> RegisterAsync(RegisterRequest request);
        Task<ServiceResult<AuthResponse>> RefreshTokenAsync(CancellationToken cancellationToken);
        Task<ServiceResult<bool>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<ServiceResult<bool>> ChangePasswordAsync(string id, ChangePasswordRequest request);
        Task<ServiceResult<bool>> ConfirmEmailAsync(ConfirmEmailRequest request);
        Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<ServiceResult<bool>> ResendConfirmationEmail(ResendConfirmationEmail request);
        Task<ServiceResult<bool>> Logout(string userId);
    }
}
