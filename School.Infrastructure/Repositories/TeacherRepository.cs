using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Domain.Entities;
using School.Infrastructure.Persistence;

namespace School.Infrastructure.Repositories;

public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Teacher>> GetAllWithSubjectAsync()
    {
        return await _dbSet
            .Include(t => t.Subject)
            .ToListAsync();
    }

    public async Task<Teacher?> GetByIdWithSubjectAsync(int id)
    {
        return await _dbSet
            .Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
}
