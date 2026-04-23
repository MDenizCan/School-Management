using Microsoft.AspNetCore.Mvc;
using School.Application.Interfaces;
using School.Contracts.Grades;

namespace School.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _service;

    public GradesController(IGradeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var grades = await _service.GetAllAsync();
        return Ok(grades);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var grade = await _service.GetByIdAsync(id);
        return Ok(grade);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGradeDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateGradeDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("transcript/{studentId}")]
    public async Task<IActionResult> GetTranscript(int studentId)
    {
        var transcript = await _service.GetTranscriptAsync(studentId);
        return Ok(transcript);
    }
}
