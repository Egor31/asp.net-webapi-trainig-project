using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using DataAccess.Repos.Interfaces;
using FluentValidation;

namespace BusinessLogic
{
    public class TeacherService : BaseService<Teacher, TeacherDto>, ITeacherService
    {
        public TeacherService(ITeacherRepo teacherRepo, IValidator<TeacherDto> teacherValidator) : 
            base(teacherRepo, teacherValidator)
        {
            
        }
    }
}