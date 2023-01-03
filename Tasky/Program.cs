using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Tasky.Data;
using Tasky.Extensions;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Mapping;
using Tasky.Models;
using Tasky.Repositories;
using Tasky.Services;
using Tasky.Validators.Category;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuth();
builder.Services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://localhost:8001")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

builder.Services.AddCustomAuthentication(builder);

builder.Services.AddAutoMapper(typeof(MappingConfiguration));

// Database connection string building
builder.Services.AddDatabase(builder);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Repositories registration
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ITaskListRepository, TaskListRepository>();

// Business logics services registration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITaskListService, TaskListService>();
builder.Services.AddScoped<IUserService, UserService>();

// Validators registration
builder.Services.AddValidatorsFromAssemblyContaining<CategoryModificationRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskyContext>();
    dbContext.Database.EnsureCreated();
}

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

app.AddGlobalErrorHandler();

app.UseCors("MyPolicy");

app.Run();
