using AutoMapper;
using School.Application.Interfaces;
using School.Contracts.Common;
using School.Contracts.Grades;
using School.Domain.Entities;

namespace School.Application.Services;

public class GradeService : IGradeService
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IGenericRepository<Teacher> _teacherRepository;
    private readonly IMapper _mapper;

    public GradeService(
        IGradeRepository gradeRepository,
        IGenericRepository<Student> studentRepository,
        IGenericRepository<Teacher> teacherRepository,
        IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _mapper = mapper;
    }

    public async Task<List<GradeDto>> GetAllAsync()
    {
        var grades = await _gradeRepository.GetAllWithDetailsAsync();
        return _mapper.Map<List<GradeDto>>(grades);
    }

    public async Task<PagedResultDto<GradeDto>> GetPagedAsync(int page, int pageSize)
    {
        var (grades, totalCount) = await _gradeRepository.GetPagedAsync(page, pageSize);
        return new PagedResultDto<GradeDto>
        {
            Items = _mapper.Map<List<GradeDto>>(grades),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<GradeDto?> GetByIdAsync(int id)
    {
        var grade = await _gradeRepository.GetByIdWithDetailsAsync(id);

        if (grade == null)
            throw new KeyNotFoundException("Grade not found");

        return _mapper.Map<GradeDto>(grade);
    }

    public async Task<GradeDto> CreateAsync(CreateGradeDto dto)
    {
        var student = await _studentRepository.GetByIdAsync(dto.StudentId);
        if (student == null)
            throw new ArgumentException("Student not found");

        var teacher = await _teacherRepository.GetByIdAsync(dto.TeacherId);
        if (teacher == null)
            throw new ArgumentException("Teacher not found");

        var grade = _mapper.Map<Grade>(dto);

        await _gradeRepository.AddAsync(grade);
        await _gradeRepository.SaveChangesAsync();

        // Reload with navigation properties
        var created = await _gradeRepository.GetByIdWithDetailsAsync(grade.Id);
        return _mapper.Map<GradeDto>(created!);
    }

    public async Task<GradeDto?> UpdateAsync(int id, UpdateGradeDto dto)
    {
        var grade = await _gradeRepository.GetByIdWithDetailsAsync(id);

        if (grade == null)
            return null;

        _mapper.Map(dto, grade);

        _gradeRepository.Update(grade);
        await _gradeRepository.SaveChangesAsync();

        // Reload to get fresh navigation data
        var updated = await _gradeRepository.GetByIdWithDetailsAsync(id);
        return _mapper.Map<GradeDto>(updated!);
    }

    public async Task DeleteAsync(int id)
    {
        var grade = await _gradeRepository.GetByIdAsync(id);

        if (grade == null)
            throw new KeyNotFoundException("Grade not found");

        _gradeRepository.Remove(grade);
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task<TranscriptDto> GetTranscriptAsync(int studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);

        if (student == null)
            throw new KeyNotFoundException("Student not found");

        var subjectAverages = await _gradeRepository.GetTranscriptAsync(studentId);

        return new TranscriptDto
        {
            StudentId = student.Id,
            StudentFullName = $"{student.FirstName} {student.LastName}",
            SubjectAverages = subjectAverages
        };
    }
}
