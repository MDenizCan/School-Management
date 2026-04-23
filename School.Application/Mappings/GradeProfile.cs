using AutoMapper;
using School.Contracts.Grades;
using School.Domain.Entities;

namespace School.Application.Mappings;

public class GradeProfile : Profile
{
    public GradeProfile()
    {
        CreateMap<Grade, GradeDto>()
            .ForMember(dest => dest.StudentFullName,
                       opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName))
            .ForMember(dest => dest.TeacherFullName,
                       opt => opt.MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName))
            .ForMember(dest => dest.SubjectName,
                       opt => opt.MapFrom(src => src.Teacher.Subject.Name));

        CreateMap<CreateGradeDto, Grade>();
        CreateMap<UpdateGradeDto, Grade>();
    }
}
