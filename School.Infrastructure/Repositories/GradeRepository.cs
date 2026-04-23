using Microsoft.EntityFrameworkCore;
using School.Application.Interfaces;
using School.Contracts.Grades;
using School.Domain.Entities;
using School.Infrastructure.Persistence;

namespace School.Infrastructure.Repositories;

public class GradeRepository : GenericRepository<Grade>, IGradeRepository
{
    public GradeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Grade>> GetAllWithDetailsAsync()
    {
        return await _dbSet
            .Include(g => g.Student)
            .Include(g => g.Teacher)
                .ThenInclude(t => t.Subject)
            .ToListAsync();
    }

    public async Task<Grade?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(g => g.Student)
            .Include(g => g.Teacher)
                .ThenInclude(t => t.Subject)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<SubjectAverageDto>> GetTranscriptAsync(int studentId)
    {
        return await _dbSet
            .Where(g => g.StudentId == studentId)
            .Include(g => g.Teacher)
                .ThenInclude(t => t.Subject)
            .GroupBy(g => g.Teacher.Subject.Name)
            .Select(group => new SubjectAverageDto
            {
                SubjectName = group.Key,
                Average = group.Average(g => g.Score)
            })
            .ToListAsync();
    }
}
