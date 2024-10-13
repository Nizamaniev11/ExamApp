using ExamApp.Data;
using ExamApp.Models;

namespace ExamApp.Services;

public class ExamService : BaseService<Exam>
{
    public ExamService(SchoolContext context) : base(context) { }
}
