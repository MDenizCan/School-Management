using School.Contracts.Grades;
using School.Domain.Entities;

namespace School.Application.Interfaces;

public interface IGradeRepository : IGenericRepository<Grade>
{
    Task<List<Grade>> GetAllWithDetailsAsync();
    Task<Grade?> GetByIdWithDetailsAsync(int id);
    Task<List<SubjectAverageDto>> GetTranscriptAsync(int studentId);
}
