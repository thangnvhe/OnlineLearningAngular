using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningAngular.DataAccess.Data;
using OnlineLearningAngular.DataAccess.Entities;
using OnlineLearningAngular.DataAccess.Settings;
using System.Text;

namespace OnlineLearningAngular.API.Configurations
{
    public static class ConfigurationService
    {
        public static void AddSqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                // Thiết lập Password (tùy chỉnh độ khó theo ý bạn)
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedEmail = true;

                // Cấu hình Lockout (khóa tài khoản khi nhập sai nhiều lần)
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // Yêu cầu Email duy nhất
                options.User.RequireUniqueEmail = true;
            })
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();

            // 3. Cấu hình Authentication & JWT
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"] ?? "Your_Very_Long_And_Secure_Secret_Key_Here";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false; // Set true nếu chạy Production có HTTPS
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static async Task AutoMigrationAsync(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await appContext.Database.MigrateAsync();
            }
        }

        public static async Task SeedData(this WebApplication webApplication, IConfiguration configuration)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

                var defaultUser = configuration.GetSection("DefaultUsers")?.Get<DefaultUser>() ?? new DefaultUser();
                var defaultRole = configuration.GetValue<string>("DefaultRole") ?? "SuperAdmin";
                try
                {
                    if (!await roleManager.RoleExistsAsync(defaultRole))
                    {
                        await roleManager.CreateAsync(new IdentityRole<int>
                        {
                            Name = defaultRole
                        });
                    }
                    var existUser = await userManager.FindByNameAsync(defaultUser.Username);
                    if (existUser == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = defaultUser.Username,
                            Email = "admin123@gmail.com",
                            FullName = "Mrs Admin",
                            Dob = DateTime.Parse("1990-01-01"),
                            Address = "123 Admin Street",
                            IsMale = true,
                            IsActive = true,
                            EmailConfirmed = true,
                            AccessFailedCount = 0
                        };
                        var identityUser = await userManager.CreateAsync(user, defaultUser.Password);

                        if (identityUser.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, defaultRole);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
