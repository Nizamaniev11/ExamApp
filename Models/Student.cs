using System.ComponentModel.DataAnnotations;

namespace ExamApp.Models;

public sealed class Student
{
    [Key]
    public int Number { get; set; }

    [Required]
    [StringLength(30)]
    public string FirstName { get; set; }

    [Required] 
    [StringLength(30)] public string LastName { get; set; }

    [Required] 
    [Range(1, 11, ErrorMessage = "Class must be between 1 and 11.")]
    public int Class { get; set; }

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    
}    