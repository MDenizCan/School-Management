using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using School.Contracts.Common;
using School.Contracts.Teachers;

namespace School.Application.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherDto>> GetAllAsync();
    Task<PagedResultDto<TeacherDto>> GetPagedAsync(int page, int pageSize);
    Task<TeacherDto?> GetByIdAsync(int id);
    Task<TeacherDto> CreateAsync(CreateTeacherDto dto);
    Task<TeacherDto?> UpdateAsync(int id, UpdateTeacherDto dto);
    Task DeleteAsync(int id);
}
