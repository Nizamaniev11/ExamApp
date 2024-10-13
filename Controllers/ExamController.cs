using Microsoft.AspNetCore.Mvc;
using ExamApp.Models;
using ExamApp.Services;

namespace ExamApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamController : ControllerBase
{
    private readonly ExamService _examService;
    private readonly ILogger<ExamController> _logger;

    public ExamController(ExamService examService, ILogger<ExamController> logger)
    {
        _examService = examService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Exam>>> GetAllExams()
    {
        try
        {
            var exams = await _examService.GetAllAsync();
            return Ok(exams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all exams.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Exam>> GetExamById(int id)
    {
        try
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam == null)
            {
                _logger.LogWarning("Exam with ID {ExamId} not found.", id);
                return NotFound();
            }
            return Ok(exam);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting exam by ID {ExamId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Exam>> AddExam(Exam exam)
    {
        try
        {
            await _examService.AddAsync(exam);
            _logger.LogInformation("Exam added successfully with ID {ExamId}.", exam.Id);
            return CreatedAtAction(nameof(GetExamById), new { id = exam.Id }, exam);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding an exam.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExam(int id, Exam exam)
    {
        try
        {
            if (id != exam.Id)
            {
                _logger.LogWarning("ID mismatch: expected {ExpectedId}, received {ReceivedId}.", id, exam.Id);
                return BadRequest();
            }

            await _examService.UpdateAsync(exam);
            _logger.LogInformation("Exam updated successfully with ID {ExamId}.", id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating exam with ID {ExamId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExam(int id)
    {
        try
        {
            await _examService.DeleteByIdAsync(id);
            _logger.LogInformation("Exam deleted successfully with ID {ExamId}.", id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting exam with ID {ExamId}.", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
