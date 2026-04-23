using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Domain.Entities;
using School.Contracts.Students;
using School.Contracts.Common;

namespace School.Application.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllAsync();
    Task<PagedResultDto<StudentDto>> GetPagedAsync(int page, int pageSize);
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(CreateStudentDto dto);
    Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto dto);
    Task DeleteAsync(int id);
}
