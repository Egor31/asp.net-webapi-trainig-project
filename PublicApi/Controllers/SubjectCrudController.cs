using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [Route("[controller]")]
    public class SubjectCrudController : BaseCrudController<SubjectDto>
    {
        public SubjectCrudController(ISubjectService subjectService) : base(subjectService)
        { }
    }
}