using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using School.Application.Interfaces;
using School.Contracts.Common;
using School.Contracts.Students;
using School.Domain.Entities;

namespace School.Application.Services;

public class StudentService : IStudentService//interface'i inherit aliyor
{
    private readonly IGenericRepository<Student> _repository;//
    private readonly IMapper _mapper;

    public StudentService(
        IGenericRepository<Student> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return _mapper.Map<List<StudentDto>>(students);
    }

    public async Task<PagedResultDto<StudentDto>> GetPagedAsync(int page, int pageSize)
    {
        var (students, totalCount) = await _repository.GetPagedAsync(page, pageSize);
        return new PagedResultDto<StudentDto>
        {
            Items = _mapper.Map<List<StudentDto>>(students),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<StudentDto> GetByIdAsync(int id)
    {// 1. Repository'den Entity'yi iste

        var student = await _repository.GetByIdAsync(id);

        if (student == null)
            throw new KeyNotFoundException("Student not found");
// 3. AutoMapper ile Entity → DTO çevir
        return _mapper.Map<StudentDto>(student);// DTO'ya çevirip döndürüyor
    }


    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    //akis su DTO geldi → Entity'ye çevir → Veritabanına kaydet → Entity'yi DTO'ya çevir → Döndür

    {   //Gelen DTO'yu Entity'ye çevir (AutoMapper ile)
        var student = _mapper.Map<Student>(dto);
        //Veritabanına ekle (repository aracılığıyla)
        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();
        //Entity'yi tekrar DTO'ya çevir ve dön
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto dto)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
            return null;

        _mapper.Map(dto, student);

        _repository.Update(student);
        await _repository.SaveChangesAsync();

        return _mapper.Map<StudentDto>(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);

        if (student == null)
            throw new ArgumentException("Invalid input");



        _repository.Remove(student);
        await _repository.SaveChangesAsync();
    }
}

