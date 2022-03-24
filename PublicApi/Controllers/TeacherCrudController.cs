using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [Route("[controller]")]
    public class TeacherCrudController : BaseCrudController<TeacherDto>
    {
        public TeacherCrudController(ITeacherService teacherService) : base(teacherService)
        { }
    }
}