using System.ComponentModel.DataAnnotations;

namespace ExamApp.Models;

public sealed class Course
{
    [Key]
    [StringLength(3)]
    public string Code { get; set; } 

    [Required]
    [StringLength(30)]
    public string Name { get; set; } 

    [Required]
    public int Class { get; set; }  

    [Required]
    [StringLength(20)]
    public string TeacherFirstName { get; set; }

    [Required]
    [StringLength(20)]
    public string TeacherLastName { get; set; } 

    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    
}