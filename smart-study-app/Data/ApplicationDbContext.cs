using Microsoft.EntityFrameworkCore;
using SmartStudyApp.Models;

namespace SmartStudyApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }
    public DbSet<Break> Breaks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}