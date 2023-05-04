using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesterStudent.Domain;
using TesterStudent.Infrastructure.Data;
using TesterStudent.Infrastructure.Models;
using TesterStudent.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TesterStudent.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Cookie")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var name = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        var user = await _context.Users.Include(ur => ur.Roles)
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(u => u.Username == name);
        return Ok(new Result<object>(true, "") { Data = new { user.Id, user.Username, user.Firstname, user.Lastname, Roles = user.Roles.Select(x => x.Role.Name) }});
    }
    [HttpGet("{id}")]
    [Authorize(Policy = "Teacher")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _context.Users.Include(ur => ur.Roles)
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(x => x.Id == id);
        if(user == null)
        {
            return Ok(new Result<object>(false, "Пользователь не найден"));
        }
        return Ok(new Result<object>(true, "") { Data = new { user.Id, user.Username, user.Firstname, user.Lastname } });
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody] LoginViewModel login)
    {
        var result = new Result<string>(false, "Пользователь не найден");
        if (!ModelState.IsValid)
        {
            return Ok(result);
        }

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == login.UserName);
        if (user is null)
        {
            return Ok(result);
        }
        var passwordHash = await PasswordBuilder.CreatePasswordAsync(login.Password, user.CreatedAt);

        if (passwordHash != user.Password)
        {
            return Ok(result);
        }
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name,user.Username),
        };
        var roles = _context.UserRoles.Include(r => r.Role)
            .Where(x => x.UserId == user.Id);
        await roles.ForEachAsync(role =>
        {
            claims.Add(new(ClaimTypes.Role, role.Role.Name));
        });
        var claimIdentity = new ClaimsIdentity(claims, "Cookie");
        var claimPrincipal = new ClaimsPrincipal(claimIdentity);
        await HttpContext.SignInAsync("Cookie", claimPrincipal);

        result.Success = true;
        result.Message = "Авторизация пройдена";
        return Ok(result);
    }
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
    {
        var result = new Result<string>(false, "Заполнены не все поля");
        if (!ModelState.IsValid)
        {
            return Ok(result);
        }
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == model.UserName);
        if (user != null)
        {
            result.Message = "Пользователь с таким именем существует";
            return Ok(result);
        }
        var date = DateTime.Now;
        var hash = await PasswordBuilder.CreatePasswordAsync(model.Password, date);

        var newUser = new User
        {
            Username = model.UserName,
            Password = hash,
            CreatedAt = date,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Roles = new List<UserRole>()
        };
        newUser.Roles.Add(new()
        {
            User = newUser,
            RoleId = 2
        });
        await _context.AddAsync(newUser);

        await _context.SaveChangesAsync();
        return await Authenticate(new LoginViewModel() { UserName = newUser.Username, Password = model.Password });
    }
    [Route("AccessDenied")]
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return Ok(new Result<object>(false, "Доступ запрещен"));
    }
}

public class Result<T>
{
    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}