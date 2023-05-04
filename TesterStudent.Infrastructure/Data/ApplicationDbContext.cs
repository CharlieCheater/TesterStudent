using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TesterStudent.Domain;
using TesterStudent.Infrastructure.Models;

namespace TesterStudent.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private static bool _initialized;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        if (!_initialized)
        {
            _initialized = true;
            Database.EnsureCreated();
        }
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Variant> Variants { get; set; }
    public DbSet<UserVariant> UserVariants { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<PossibleAnswer> PossibleAnswers { get; set; }

    protected override async void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new List<Role>
        {
            new() { Id = 1, Name = "Преподаватель" },
            new() { Id = 2, Name = "Студент" }
        });
        var dt = DateTime.Now;
        var passwordHash = await PasswordBuilder.CreatePasswordAsync("03032002MNV", dt);
        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new () { Id = 1, CreatedAt = dt, Username = "Teacher", Firstname = "Charlie", Lastname = "Cheater", Password = passwordHash }
        });
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => new { x.RoleId, x.UserId });
            entity.HasOne(r => r.Role).WithMany(u => u.Users).HasForeignKey(x => x.RoleId);
            entity.HasOne(u => u.User).WithMany(r => r.Roles).HasForeignKey(x => x.UserId);

            entity.HasData(new List<UserRole>
            {
                new() { RoleId = 1, UserId = 1 }
            });
        });
        modelBuilder.Entity<UserVariant>(entity =>
        {
            entity.HasKey(x => new { x.VariantId, x.UserId });
            entity.HasOne(v => v.Variant).WithMany(u =>u.Users).HasForeignKey(i => i.VariantId);
            entity.HasOne(v => v.User).WithMany(u =>u.Variants).HasForeignKey(i => i.UserId);
        });
        AddVariantWithExercises(modelBuilder);
    }
    protected void AddVariantWithExercises(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>().HasData(new List<Test>()
        {
            new Test() { Id = 1, Name = "Основы ASP.NET Core", Description = "Тест на темы на знание основ ASP.NET Core и Entity Framework Core" }
        });
        modelBuilder.Entity<Variant>().HasData(new List<Variant>()
        {
            new Variant() { Id = 1, Name = "Первый вариант", TestId = 1 },
        });
        modelBuilder.Entity<Exercise>().HasData(new List<Exercise>()
        {
            new Exercise() { Id = 1, Description = "Какой из следующих методов контроллера в Asp Net Core Mvc отвечает за обработку HTTP GET запроса?", VariantId = 1 },
            new Exercise() { Id = 2, Description = "Какой из следующих типов соответствует базовому классу для всех классов контекста данных в Entity Framework?", VariantId = 1 },
            new Exercise() { Id = 3, Description = "Что такое DbSet в Entity Framework?", VariantId = 1 },
            new Exercise() { Id = 4, Description = "Какой файл используется для конфигурирования приложения ASP.NET Core MVC?", VariantId = 1 },
            new Exercise() { Id = 5, Description = "Какой протокол используется для передачи данных в ASP.NET Core MVC?", VariantId = 1 },
        });

        modelBuilder.Entity<PossibleAnswer>().HasData(new List<PossibleAnswer>()
        {
            new PossibleAnswer() { Id = 1, ExerciseId = 1, Value = "Index()", IsCorrect = true },
            new PossibleAnswer() { Id = 2, ExerciseId = 1, Value = "Create()", IsCorrect = false },
            new PossibleAnswer() { Id = 3, ExerciseId = 1, Value = "Edit()", IsCorrect = false },
            new PossibleAnswer() { Id = 4, ExerciseId = 1, Value = "Delete()", IsCorrect = false },

            new PossibleAnswer() { Id = 5, ExerciseId = 2, Value = "DataContext", IsCorrect = false },
            new PossibleAnswer() { Id = 6, ExerciseId = 2, Value = "DbSet", IsCorrect = false },
            new PossibleAnswer() { Id = 7, ExerciseId = 2, Value = "MainContext", IsCorrect = false },
            new PossibleAnswer() { Id = 8, ExerciseId = 2, Value = "DbContext", IsCorrect = true },

            new PossibleAnswer() { Id = 9, ExerciseId = 3, Value = "Свойство, представляющее сеть в сетевой топологии", IsCorrect = false },
            new PossibleAnswer() { Id = 10, ExerciseId = 3, Value =  "Свойство, представляющее таблицу в базе данных", IsCorrect = true },
            new PossibleAnswer() { Id = 11, ExerciseId = 3, Value = "Свойство, представляющее файл в файловой системе", IsCorrect = false },
            
            new PossibleAnswer() { Id = 12, ExerciseId = 4, Value = "a) app.config", IsCorrect = false },
            new PossibleAnswer() { Id = 13, ExerciseId = 4, Value = "b) web.config", IsCorrect = false },
            new PossibleAnswer() { Id = 14, ExerciseId = 4, Value = "c) appsettings.json", IsCorrect = true },
            
            new PossibleAnswer() { Id = 15, ExerciseId = 5, Value = "TCP/IP", IsCorrect = false },
            new PossibleAnswer() { Id = 16, ExerciseId = 5, Value = "HTTP", IsCorrect = false },
            new PossibleAnswer() { Id = 17, ExerciseId = 5, Value = "FTP", IsCorrect = true },
        });
    }
}