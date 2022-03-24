using System.ComponentModel.DataAnnotations;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [Route("[controller]")]
    public class StudentCrudController : BaseCrudController<StudentDto>
    {
        public StudentCrudController(IStudentService studentService) : base(studentService)
        { }
    }
}