using AutoMapper;
using BusinessLogic.ModelsDto;
using DataAccess.Models;

namespace BusinessLogic.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<HomeWork, HomeWorkDto>().ReverseMap();
            CreateMap<RollBookRecord, RollBookRecordDto>().ReverseMap();
        }
    }
}