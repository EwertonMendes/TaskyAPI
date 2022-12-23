using Microsoft.EntityFrameworkCore;
using Tasky.Data;
using Tasky.Extensions;
using Tasky.Interfaces.Repositories;
using Tasky.Interfaces.Services;
using Tasky.Mapping;
using Tasky.Repositories;
using Tasky.Services;
using Tasky.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingConfiguration));

// Database connection string building
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<TaskyContext>(options => options.UseSqlServer(connectionString));

// Repositories registration
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ITaskListRepository, TaskListRepository>();

// Business logics services registration
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITaskListService, TaskListService>();

// Validators registration
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

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
