using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TesterStudent.Domain;
using TesterStudent.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddAuthentication("Cookie")
    .AddCookie("Cookie", options =>
    {
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(200);
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Teacher", builder =>
    {
        builder.RequireClaim(ClaimTypes.Role, "Преподаватель");
    });

    options.AddPolicy("Student", builder =>
    {
        builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Студент")
                                      || x.User.HasClaim(ClaimTypes.Role, "Преподаватель"));
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
