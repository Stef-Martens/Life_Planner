using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LifePlanner.Server.Data;
using LifePlanner.Server.Repositories;
using LifePlanner.Server.Repositories.Interfaces;
using LifePlanner.Server.Services.Interfaces;
using LifePlanner.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql;
using System;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// check if on windows to see which connection string to use
var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

// Select connection string based on the operating system
var connectionString = isWindows 
    ? builder.Configuration.GetConnectionString("LifePlannerServerContext") 
    : builder.Configuration.GetConnectionString("LifePlannerServerContextDocker");


if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string not found.");
}

// Configure the DbContext based on the OS
if (isWindows)
{
    // Use SQL Server on Windows
    builder.Services.AddDbContext<LifePlannerServerContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Use MySQL on non-Windows platforms (like macOS)
    builder.Services.AddDbContext<LifePlannerServerContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

// Add authentication and JWT Bearer services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://dev-skr3fnrj.eu.auth0.com"; // Replace with your Auth0 domain
        options.Audience = "https://dev-skr3fnrj.eu.auth0.com/api/v2/"; // Replace with your API audience (from Auth0 settings)
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://dev-skr3fnrj.eu.auth0.com", // Replace with your Auth0 domain
            ValidateAudience = true,
            ValidAudience = "https://dev-skr3fnrj.eu.auth0.com/api/v2/", // Replace with your API audience
            ValidateLifetime = true, // Validate token expiration
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("Token validated successfully.");
                return Task.CompletedTask;
            }
        };
    });
// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
// });

builder.Services.AddControllers();

builder.Services.AddAuthorization();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

// Add authentication and authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");


app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
