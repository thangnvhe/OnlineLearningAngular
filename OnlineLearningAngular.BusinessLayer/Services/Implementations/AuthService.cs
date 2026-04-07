using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningAngular.BusinessLayer.Dtos.Requests.Auths;
using OnlineLearningAngular.BusinessLayer.Dtos.Responses;
using OnlineLearningAngular.BusinessLayer.Services.Interfaces;
using OnlineLearningAngular.DataAccess.Common;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Repositories.Interfaces;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineLearningAngular.BusinessLayer.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string _frontEndUrl;
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly IValidator<ForgotPasswordRequest> _forgotPasswordRequestValidator;
        private readonly IValidator<ResetPasswordRequest> _resetPasswordRequestValidator;
        private readonly IValidator<ConfirmEmailRequest> _confirmEmailRequestValidator;
        private readonly IValidator<ChangePasswordRequest> _changePasswordRequestValidator;
        private readonly IValidator<ResendConfirmationEmail> _resendConfirmationEmailValidator;

        private static readonly ConcurrentDictionary<string, string> RefreshTokenStore = new();

        public AuthService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IMapper mapper,
            IConfiguration configuration,
            IValidator<RegisterRequest> registerRequestValidator,
            IValidator<LoginRequest> loginRequestValidator,
            IValidator<ForgotPasswordRequest> forgotPasswordRequestValidator,
            IValidator<ResetPasswordRequest> resetPasswordRequestValidator,
            IValidator<ConfirmEmailRequest> confirmEmailRequestValidator,
            IValidator<ChangePasswordRequest> changePasswordRequestValidator,
            IValidator<ResendConfirmationEmail> resendConfirmationEmailValidator)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
            _frontEndUrl = _configuration["FrontEndUrl"] ?? "http://localhost:3000";
            _registerRequestValidator = registerRequestValidator;
            _loginRequestValidator = loginRequestValidator;
            _forgotPasswordRequestValidator = forgotPasswordRequestValidator;
            _resetPasswordRequestValidator = resetPasswordRequestValidator;
            _confirmEmailRequestValidator = confirmEmailRequestValidator;
            _changePasswordRequestValidator = changePasswordRequestValidator;
            _resendConfirmationEmailValidator = resendConfirmationEmailValidator;
        }

        public async Task<ServiceResult<bool>> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Dữ liệu thay đổi mật khẩu không hợp lệ.");

            var validationResult = await _changePasswordRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return ServiceResult<bool>.Failure("Người dùng không tồn tại");
            }

            var changeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (!changeResult.Succeeded)
                return ServiceResult<bool>.Failure("Không thể thay đổi mật khẩu.");

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Dữ liệu xác nhận email không hợp lệ.");

            var validationResult = await _confirmEmailRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return ServiceResult<bool>.Failure("Người dùng không tồn tại.");

            if (user.EmailConfirmed)
                return ServiceResult<bool>.Failure("Email đã được xác nhận.");

            var decodedToken = HttpUtility.UrlDecode(request.Token);
            var confirmResult = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!confirmResult.Succeeded)
                return ServiceResult<bool>.Failure("Token xác nhận không hợp lệ hoặc đã hết hạn.");

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Email không hợp lệ.");

            var validationResult = await _forgotPasswordRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return ServiceResult<bool>.Failure("Email không tồn tại.");

            if (!user.EmailConfirmed)
                return ServiceResult<bool>.Failure("Email chưa được xác thực, bạn cần xác thực email trước khi thực hiện thao tác này");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);

            var resetLink = $"{_frontEndUrl}/reset-password?userId={user.Id}&token={encodedToken}";

            try
            {
                await _emailService.SendTemplateEmailAsync(
                    user.Email!,
                    "Yêu cầu đặt lại mật khẩu",
                    "ResetPassword",
                    new Dictionary<string, string>
                    {
                        { "AppName", "Online Learning Platform" },
                        { "UserId", user.Id.ToString() },
                        { "Token", encodedToken },
                        { "ResetLink", resetLink }
                    });
            }
            catch (Exception)
            {
                return ServiceResult<bool>.Failure("Hệ thống đang bận hoặc gặp sự cố kết nối. Vui lòng thử lại sau vài phút.");
            }
            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<AuthResponse>> LoginAsync(LoginRequest request)
        {
            if (request == null)
                return ServiceResult<AuthResponse>.Failure("Tên đăng nhập và mật khẩu không được để trống.");

            var validationResult = await _loginRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<AuthResponse>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            // 1. Tìm kiếm user theo userName bằng UserManager
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
                return ServiceResult<AuthResponse>.Failure("Tên đăng nhập hoặc mật khẩu không đúng.");

            // 2. Dùng SignInManager để check pass 
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (signInResult.IsNotAllowed)
            {
                return ServiceResult<AuthResponse>.Failure("Tài khoản chưa được xác nhận email. Vui lòng kiểm tra email để xác nhận tài khoản.");
            }

            if (!signInResult.Succeeded)
            {
                return ServiceResult<AuthResponse>.Failure("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = GenerateJwtToken(user, roles);

            var refreshToken = GenerateRefreshToken();

            var response = _mapper.Map<AuthResponse>(user);
            response.AccessToken = accessToken;

            var refreshTokenKey = $"refresh_token:{refreshToken}";
            RefreshTokenStore[refreshTokenKey] = user.Id.ToString();

            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            response.Roles = roles.ToList();

            return ServiceResult<AuthResponse>.Success(response);
        }

        public Task<ServiceResult<bool>> Logout(string userId)
        {
            // 1. Lấy Refresh Token từ Cookie
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                // 2. Xóa Refresh Token khỏi Redis
                var refreshTokenKey = $"refresh_token:{refreshToken}";
                RefreshTokenStore.TryRemove(refreshTokenKey, out _);
            }

            // 3. Xóa Cookie ở trình duyệt (Ghi đè bằng cookie hết hạn)
            _httpContextAccessor.HttpContext!.Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            // 4. (Tùy chọn) Xóa các thông tin khác trong DB nếu bạn vẫn muốn lưu ở DB
            // Nhưng với Redis thì bước 2 là quan trọng nhất rồi.

            return Task.FromResult(ServiceResult<bool>.Success(true));
        }

        public async Task<ServiceResult<AuthResponse>> RefreshTokenAsync(CancellationToken cancellationToken)
        {
            // 1. Lấy token từ Cookie
            var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return ServiceResult<AuthResponse>.Failure("Refresh token không tồn tại.");

            // 2. [REDIS] Kiểm tra Token trong Redis xem có tồn tại không và lấy UserId ra
            var refreshTokenCacheKey = $"refresh_token:{refreshToken}";
            RefreshTokenStore.TryGetValue(refreshTokenCacheKey, out var userIdString);

            if (string.IsNullOrEmpty(userIdString))
                return ServiceResult<AuthResponse>.Failure("Refresh token không hợp lệ hoặc đã hết hạn.");

            // 3. Tìm User chuẩn từ DB bằng Id (Chỉ đọc 1 bản ghi duy nhất, rất nhanh)
            var user = await _userManager.FindByIdAsync(userIdString);
            if (user == null)
                return ServiceResult<AuthResponse>.Failure("Người dùng không tồn tại.");

            // 4. Lấy Roles (Ưu tiên Redis như bạn đã làm)
            var roles = await _userManager.GetRolesAsync(user);

            // 5. Tạo Access Token mới và Refresh Token mới
            var newAccessToken = GenerateJwtToken(user, roles);

            var newRefreshToken = GenerateRefreshToken();

            // 6. [REDIS] Xóa Token cũ và Lưu Token mới vào Redis
            RefreshTokenStore.TryRemove(refreshTokenCacheKey, out _); // Xóa cái cũ cho sạch
            var newRefreshTokenCacheKey = $"refresh_token:{newRefreshToken}";
            RefreshTokenStore[newRefreshTokenCacheKey] = user.Id.ToString();

            // 7. Cập nhật Cookie
            _httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            // 8. Map kết quả trả về
            var response = _mapper.Map<AuthResponse>(user);
            response.AccessToken = newAccessToken;
            response.Roles = roles.ToList();

            return ServiceResult<AuthResponse>.Success(response);
        }

        public async Task<ServiceResult<bool>> RegisterAsync(RegisterRequest request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Dữ liệu đăng ký không hợp lệ.");

            var validationResult = await _registerRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUserByEmail != null)
                return ServiceResult<bool>.Failure("Email đã tồn tại. Vui lòng sử dụng email khác.");

            var existingUserByUserName = await _userManager.FindByNameAsync(request.Username);

            if (existingUserByUserName != null)
                return ServiceResult<bool>.Failure("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");

            var newUser = _mapper.Map<ApplicationUser>(request);

            var createResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createResult.Succeeded)
            {
                return ServiceResult<bool>.Failure("Đăng ký thất bại.");
            }

            var roleResult = await _userManager.AddToRoleAsync(newUser, Utilizer.Role_Student);

            if (!roleResult.Succeeded)
                return ServiceResult<bool>.Failure("Đăng ký thất bại. Lỗi phân quyền");

            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var encodedToken = HttpUtility.UrlEncode(token);

                await _emailService.SendTemplateEmailAsync(
                    newUser.Email!,
                    "Chào mừng bạn đến với Online Learning Platform",
                    "Welcome",
                    new Dictionary<string, string>
                    {
                        { "AppName", "Online Learning Platform" },
                        { "UserName", newUser.UserName! },
                        { "LoginUrl", $"{_frontEndUrl}/login" }
                    });

                var confirmationLink = $"{_frontEndUrl}?userId={newUser.Id}&token={encodedToken}";

                await _emailService.SendTemplateEmailAsync(
                    newUser.Email!,
                    "Xác nhận email đăng ký",
                    "EmailConfirmation",
                    new Dictionary<string, string>
                    {
                        { "AppName", "Online Learning Platform" },
                        { "UserName", newUser.UserName! },
                        { "ConfirmationLink", confirmationLink }
                    });
            }
            catch (Exception)
            {
                return ServiceResult<bool>.Failure("Đăng ký thành công nhưng gửi email xác nhận thất bại. Vui lòng bấm 'Gửi lại mã xác nhận' tại trang đăng nhập.");
            }

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> ResendConfirmationEmail(ResendConfirmationEmail request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Tên đăng nhập không được để trống.");

            var validationResult = await _resendConfirmationEmailValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
                return ServiceResult<bool>.Failure("Tên đăng nhập không tồn tại.");

            if (user.EmailConfirmed)
                return ServiceResult<bool>.Failure("Tài khoản này đã được xác nhận email từ trước. Vui lòng đăng nhập.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = HttpUtility.UrlEncode(token);
            var confirmationLink = $"{_frontEndUrl}?userId={user.Id}&token={encodedToken}";

            await _emailService.SendTemplateEmailAsync(
                user.Email!,
                "Xác nhận email đăng ký",
                "EmailConfirmation",
                new Dictionary<string, string>
                {
                    { "AppName", "Online Learning Platform" },
                    { "UserName", user.UserName! },
                    { "ConfirmationLink", confirmationLink }
                });

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (request == null)
                return ServiceResult<bool>.Failure("Dữ liệu thay đổi mật khẩu không hợp lệ.");

            var validationResult = await _resetPasswordRequestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return ServiceResult<bool>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return ServiceResult<bool>.Failure("Người dùng không tồn tại.");

            var decodedToken = HttpUtility.UrlDecode(request.Token);
            var resetResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);

            if (!resetResult.Succeeded)
                return ServiceResult<bool>.Failure("Không thể thay đổi mật khẩu. " + string.Join("; ", resetResult.Errors.Select(e => e.Description)));

            return ServiceResult<bool>.Success(true);
        }

        private string GenerateJwtToken(ApplicationUser user, IList<string> roles)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? throw new Exception("JWT Secret Key is missing!");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                // Dùng ClaimTypes.NameIdentifier cho UserId để đồng bộ với Identity
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID duy nhất cho mỗi Token
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // SỬA QUAN TRỌNG: Dùng UtcNow và sửa đúng tên ExpiryMinutes
            var expiryMinutes = Convert.ToDouble(jwtSettings["ExpiryMinutes"] ?? "60");

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow, // Token có hiệu lực ngay bây giờ
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes), // Hết hạn theo giờ chuẩn UTC
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
