using Microsoft.AspNetCore.Identity;
using SimpleAuthApp.Services;
using SimpleAuthApp.Services.Repository;
using SimpleAuthApp.Services.Services;
using SimpleAuthApp.Services.Validators;
using SimpleAuthApp.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<UserValidator>();
builder.Services.AddSingleton<JWTService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
