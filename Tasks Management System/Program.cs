using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tasks_Management_System.API.Extensions;
using Tasks_Management_System.Application.Common.Mapping;
using Tasks_Management_System.Application.Interfaces.Auth;
using Tasks_Management_System.Application.Interfaces.Sections;
using Tasks_Management_System.Application.Interfaces.Tasks;
using Tasks_Management_System.Application.InterfacesServices;
using Tasks_Management_System.Application.Mappings;
using Tasks_Management_System.Application.Services;
using Tasks_Management_System.Application.Validators;
using Tasks_Management_System.Domain.Entities;
using Tasks_Management_System.Infrastructure.Data;
using Tasks_Management_System.Infrastructure.Repositories;
using Tasks_Management_System.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ===== DbContext =====
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ===== Identity =====
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ===== Repositories =====
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// ===== Services =====
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// ===== AutoMapper =====
builder.Services.AddAutoMapper(typeof(AuthMappingProfile));
builder.Services.AddAutoMapper(typeof(SectionMappingProfile));
builder.Services.AddAutoMapper(typeof(TaskMappingProfile));

// ===== FluentValidation =====
builder.Services.AddScoped<SectionValidator>();
builder.Services.AddScoped<TaskValidator>();

// ===== Controllers =====
builder.Services.AddControllers();

// ===== JWT Authentication (Extension) =====
builder.Services.AddJwtAuthentication(builder.Configuration);

// ===== Swagger =====
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ===== Middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<ApiResponseMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();