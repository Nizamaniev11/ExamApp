using ExamApp.Data;
using ExamApp.Models;

namespace ExamApp.Services;

public class StudentService : BaseService<Student>
{
    public StudentService(SchoolContext context) : base(context) { }
}
