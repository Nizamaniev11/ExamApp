using ExamApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Exam> Exams { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Student>()
            .HasKey(s => s.Number); 

        modelBuilder.Entity<Course>()
            .HasKey(c => c.Code); 

        modelBuilder.Entity<Exam>()
            .HasKey(e => e.Id); 

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Exams)
            .WithOne(e => e.Student)
            .HasForeignKey(e => e.StudentId); 

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Exams)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseCode); 
    }
    
}