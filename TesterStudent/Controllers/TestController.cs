using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesterStudent.Infrastructure.Data;
using TesterStudent.Infrastructure.Models;
using TesterStudent.Models;

namespace TesterStudent.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Cookie")]
public class TestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("getTests")]
    public async Task<IActionResult> GetAllTests()
    {
        var id = HttpContext.User.Claims.First(x => x.Type == "Id");
        var tests = await _context.Tests.Include(v => v.Variants).ThenInclude(u => u.Users).ToListAsync();
        var viewModel = tests.Select(t => new
        {
            t.Id,
            t.Name,
            t.Description,
            AllowedVariants = t.Variants.Where(x => !x.Users.Any(u => u.UserId.ToString() == id.Value)).Select(v => new
            {
                v.Id,
                v.Name
            }),
        });
        var result = new Result<object>(true, "Все тесты");
        result.Data = viewModel;
        return Ok(result);
    }
    [HttpGet]
    [Route("getTestVariants")]
    public async Task<IActionResult> GetTestVariants([FromQuery] int id)
    {
        var result = new Result<object>(false, "Тест не оформлен");
        var variants = await _context.Variants.Select(x => new { x.Id, x.Name }).ToListAsync();
        result.Success = variants.Any();
        if (!result.Success)
        {
            return Ok(result);
        }
        result.Message = "";
        result.Data = variants;
        return Ok(result);
    }
    [HttpGet]
    [Route("GetExercises")]
    public async Task<IActionResult> GetExercises([FromQuery] int id)
    {
        var result = new Result<IEnumerable<ExerciseViewModel>>(false, "Задания по данному варианту - не найдены");
        var exercises = await _context.Exercises.Include(x => x.Answers)
            .Where(x => x.VariantId == id)
            .ToListAsync();
        if(!exercises.Any())
        {
            return Ok(result);
        }
        var viewModel = exercises.Select(e => new ExerciseViewModel
        {
            Id = e.Id,
            Description = e.Description,
            Answers = e.Answers.Select(x => new AnswerViewModel
            {
                Id = x.Id,
                Value = x.Value
            }).ToList(),
            HasMultipleCorrectAnswers = e.Answers.Where(a => a.IsCorrect).Count() > 1
        });

        result.Success = true;
        result.Data = viewModel;
        result.Message = "";

        return Ok(result);
    }

}