using School.Contracts.Common;
using School.Contracts.Grades;

namespace School.Application.Interfaces;

public interface IGradeService
{
    Task<List<GradeDto>> GetAllAsync();
    Task<PagedResultDto<GradeDto>> GetPagedAsync(int page, int pageSize);
    Task<GradeDto?> GetByIdAsync(int id);
    Task<GradeDto> CreateAsync(CreateGradeDto dto);
    Task<GradeDto?> UpdateAsync(int id, UpdateGradeDto dto);
    Task DeleteAsync(int id);
    Task<TranscriptDto> GetTranscriptAsync(int studentId);
}
