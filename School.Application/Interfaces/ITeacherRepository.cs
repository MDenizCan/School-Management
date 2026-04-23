using School.Domain.Entities;

namespace School.Application.Interfaces;

public interface ITeacherRepository : IGenericRepository<Teacher>
{
    Task<List<Teacher>> GetAllWithSubjectAsync();
    Task<Teacher?> GetByIdWithSubjectAsync(int id);
}
