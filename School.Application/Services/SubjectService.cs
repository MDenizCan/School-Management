using AutoMapper;
using School.Application.Interfaces;
using School.Contracts.Common;
using School.Contracts.Subjects;
using School.Domain.Entities;

namespace School.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly IGenericRepository<Subject> _repository;
    private readonly IMapper _mapper;

    public SubjectService(
        IGenericRepository<Subject> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<SubjectDto>> GetAllAsync()
    {
        var subjects = await _repository.GetAllAsync();
        return _mapper.Map<List<SubjectDto>>(subjects);
    }

    public async Task<PagedResultDto<SubjectDto>> GetPagedAsync(int page, int pageSize)
    {
        var (subjects, totalCount) = await _repository.GetPagedAsync(page, pageSize);
        return new PagedResultDto<SubjectDto>
        {
            Items = _mapper.Map<List<SubjectDto>>(subjects),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<SubjectDto?> GetByIdAsync(int id)
    {
        var subject = await _repository.GetByIdAsync(id);

        if (subject == null)
            throw new KeyNotFoundException("Subject not found");

        return _mapper.Map<SubjectDto>(subject);
    }

    public async Task<SubjectDto> CreateAsync(CreateSubjectDto dto)
    {
        var subject = _mapper.Map<Subject>(dto);

        await _repository.AddAsync(subject);
        await _repository.SaveChangesAsync();

        return _mapper.Map<SubjectDto>(subject);
    }

    public async Task<SubjectDto?> UpdateAsync(int id, UpdateSubjectDto dto)
    {
        var subject = await _repository.GetByIdAsync(id);

        if (subject == null)
            return null;

        _mapper.Map(dto, subject);

        _repository.Update(subject);
        await _repository.SaveChangesAsync();

        return _mapper.Map<SubjectDto>(subject);
    }

    public async Task DeleteAsync(int id)
    {
        var subject = await _repository.GetByIdAsync(id);

        if (subject == null)
            throw new KeyNotFoundException("Subject not found");

        _repository.Remove(subject);
        await _repository.SaveChangesAsync();
    }
}
