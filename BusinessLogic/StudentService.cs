using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using DataAccess.Repos.Interfaces;
using FluentValidation;

namespace BusinessLogic
{
    public class StudentService : BaseService<Student, StudentDto>, IStudentService
    {
        public StudentService(IStudentRepo studentRepo, IValidator<StudentDto> studentValidator) : 
            base(studentRepo, studentValidator)
        {
            
        }
    }
}