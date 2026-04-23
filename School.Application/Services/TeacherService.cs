using AutoMapper;
using School.Application.Interfaces;
using School.Contracts.Common;
using School.Contracts.Teachers;
using School.Domain.Entities;

namespace School.Application.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;
    private readonly IGenericRepository<Subject> _subjectRepository;
    private readonly IMapper _mapper;

    public TeacherService(
        ITeacherRepository repository,
        IGenericRepository<Subject> subjectRepository,
        IMapper mapper)
    {
        _repository = repository;
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }

    public async Task<List<TeacherDto>> GetAllAsync()
    {
        var teachers = await _repository.GetAllWithSubjectAsync();
        return _mapper.Map<List<TeacherDto>>(teachers);
    }

    public async Task<PagedResultDto<TeacherDto>> GetPagedAsync(int page, int pageSize)
    {
        var (teachers, totalCount) = await _repository.GetPagedAsync(page, pageSize);
        return new PagedResultDto<TeacherDto>
        {
            Items = _mapper.Map<List<TeacherDto>>(teachers),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<TeacherDto?> GetByIdAsync(int id)
    {
        var teacher = await _repository.GetByIdWithSubjectAsync(id);

        if (teacher == null)
            throw new KeyNotFoundException("Teacher not found");

        return _mapper.Map<TeacherDto>(teacher);
    }

    public async Task<TeacherDto> CreateAsync(CreateTeacherDto dto)
    {
        var subject = await _subjectRepository.GetByIdAsync(dto.SubjectId);

        if (subject == null)
            throw new ArgumentException("Subject does not exist");

        var teacher = _mapper.Map<Teacher>(dto);

        await _repository.AddAsync(teacher);
        await _repository.SaveChangesAsync();

        // Reload with Subject navigation
        var created = await _repository.GetByIdWithSubjectAsync(teacher.Id);
        return _mapper.Map<TeacherDto>(created!);
    }

    public async Task<TeacherDto?> UpdateAsync(int id, UpdateTeacherDto dto)
    {
        var teacher = await _repository.GetByIdWithSubjectAsync(id);

        if (teacher == null)
            return null;

        var subject = await _subjectRepository.GetByIdAsync(dto.SubjectId);
        if (subject == null)
            throw new ArgumentException("Subject does not exist");

        _mapper.Map(dto, teacher);

        _repository.Update(teacher);
        await _repository.SaveChangesAsync();

        // Reload with Subject navigation
        var updated = await _repository.GetByIdWithSubjectAsync(id);
        return _mapper.Map<TeacherDto>(updated!);
    }

    public async Task DeleteAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(id);

        if (teacher == null)
            throw new KeyNotFoundException("Teacher not found");

        _repository.Remove(teacher);
        await _repository.SaveChangesAsync();
    }
}
