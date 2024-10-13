

using System.ComponentModel.DataAnnotations;

namespace ExamApp.Models;

public sealed class Exam
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudentNumber { get; set; } 

    [Required]
    public DateTime Date { get; set; } 

    [Required]
    [Range(1, 5)]
    public int Grade { get; set; }
    
    [Required]
    public string CourseCode { get; set; }

    public Course Course { get; set; } 
    
    [Required]
    public int StudentId { get; set; }
    public Student Student { get; set; } 
    
}
