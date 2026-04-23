using AutoMapper;
using School.Contracts.Subjects;
using School.Domain.Entities;

namespace School.Application.Mappings;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDto>();
        CreateMap<CreateSubjectDto, Subject>();
        CreateMap<UpdateSubjectDto, Subject>();
    }
}
