using Microsoft.AspNetCore.Mvc;
using ExamApp.Models;
using ExamApp.Services;

namespace ExamApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    private readonly ILogger<StudentController> _logger;

    public StudentController(StudentService studentService, ILogger<StudentController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        try
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all students.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudentById(int id)
    {
        try
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found.", id);
                return NotFound();
            }
            return Ok(student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting student by ID {StudentId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Student>> AddStudent(Student student)
    {
        try
        {
            await _studentService.AddAsync(student);
            _logger.LogInformation("Student added successfully with ID {StudentId}.", student.Number);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Number }, student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a student.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        try
        {
            if (id != student.Number)
            {
                _logger.LogWarning("ID mismatch: expected {ExpectedId}, received {ReceivedId}.", id, student.Number);
                return BadRequest();
            }

            await _studentService.UpdateAsync(student);
            _logger.LogInformation("Student updated successfully with ID {StudentId}.", id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating student with ID {StudentId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            await _studentService.DeleteByIdAsync(id);
            _logger.LogInformation("Student deleted successfully with ID {StudentId}.", id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting student with ID {StudentId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
