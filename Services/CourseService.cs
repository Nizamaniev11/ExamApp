using ExamApp.Data;
using ExamApp.Models;

namespace ExamApp.Services;

public class CourseService : BaseService<Course>
{
    public CourseService(SchoolContext context) : base(context) { }
}
