using School.Contracts.Common;
using School.Contracts.Subjects;

namespace School.Application.Interfaces;

public interface ISubjectService
{
    Task<List<SubjectDto>> GetAllAsync();
    Task<PagedResultDto<SubjectDto>> GetPagedAsync(int page, int pageSize);
    Task<SubjectDto?> GetByIdAsync(int id);
    Task<SubjectDto> CreateAsync(CreateSubjectDto dto);
    Task<SubjectDto?> UpdateAsync(int id, UpdateSubjectDto dto);
    Task DeleteAsync(int id);
}
