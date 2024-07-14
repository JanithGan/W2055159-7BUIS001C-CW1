using Microsoft.EntityFrameworkCore;
using SmartStudyApp.Models;

namespace SmartStudyApp.Data;

public static class ApplicationDbContextSeed
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context =
            new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Students.Any()) return; // DB has been seeded

        var availableModules = new Module[]
        {
            new() { Name = "Advanced Software Design Core", ModuleCode = "7SENG004C", Credits = 20 },
            new() { Name = "Concurrent and Distributed Systems", ModuleCode = "7SENG007C", Credits = 20 },
            new() { Name = "Enterprise Application Development", ModuleCode = "7SENG001C", Credits = 20 },
            new() { Name = "Data Mining and Machine Learning", ModuleCode = "7BUIS008C", Credits = 20 },
            new() { Name = "Cloud Computing Applications", ModuleCode = "7BUIS027C", Credits = 20 },
            new() { Name = "MSc Project", ModuleCode = "7SENG004C", Credits = 60 }
        };

        context.Modules.AddRange(availableModules);

        context.Students.AddRange(
            new Student
            {
                StudentId = 1,
                Name = "John Doe",
                StudentNumber = "S1234567",
                EnrolledModuleCodes = ["7SENG004C", "7SENG007C", "7SENG001C"]
            }
        );

        context.SaveChanges();
    }
}