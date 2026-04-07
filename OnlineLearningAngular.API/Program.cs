using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using OnlineLearningAngular.API.Configurations;
using OnlineLearningAngular.BusinessLayer.Authorization;
using OnlineLearningAngular.BusinessLayer.Mapper;
using OnlineLearningAngular.DataAccess.Data.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});

// Đăng ký Policy Provider và Handler
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// add sql configuration
builder.Services.AddSqlServerConfiguration(builder.Configuration);

// add DI
builder.Services.AddDependenceInjection();

// add automapper
builder.Services.AddAutoMapper(typeof(AutoMappers));

// add Fulent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.AutoMigrationAsync();
await app.SeedData(builder.Configuration);
SeedDatabase();

app.Run();


void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        scope.ServiceProvider.GetRequiredService<IDbInitializer>().Initialize();
    }
}