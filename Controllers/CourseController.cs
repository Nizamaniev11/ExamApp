using Microsoft.AspNetCore.Mvc;
using ExamApp.Models;
using ExamApp.Services;

namespace ExamApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly ILogger<CourseController> _logger;

    public CourseController(CourseService courseService, ILogger<CourseController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
    {
        try
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all courses.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<Course>> GetCourseByCode(string code)
    {
        try
        {
            var course = await _courseService.GetByIdAsync(code);
            if (course == null)
            {
                _logger.LogWarning("Course with code {CourseCode} not found.", code);
                return NotFound();
            }
            return Ok(course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting course by code {CourseCode}.", code);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Course>> AddCourse(Course course)
    {
        try
        {
            await _courseService.AddAsync(course);
            _logger.LogInformation("Course added successfully with code {CourseCode}.", course.Code);
            return CreatedAtAction(nameof(GetCourseByCode), new { code = course.Code }, course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding a course.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> UpdateCourse(string code, Course course)
    {
        try
        {
            if (code != course.Code)
            {
                _logger.LogWarning("Code mismatch: expected {ExpectedCode}, received {ReceivedCode}.", code, course.Code);
                return BadRequest();
            }

            await _courseService.UpdateAsync(course);
            _logger.LogInformation("Course updated successfully with code {CourseCode}.", code);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating course with code {CourseCode}.", code);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{code}")]
    public async Task<IActionResult> DeleteCourse(string code)
    {
        try
        {
            await _courseService.DeleteByIdAsync(code);
            _logger.LogInformation("Course deleted successfully with code {CourseCode}.", code);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting course with code {CourseCode}.", code);
            return StatusCode(500, "Internal server error");
        }
    }
}
