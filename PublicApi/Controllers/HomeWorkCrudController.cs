using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [Route("[controller]")]
    public class HomeWorkCrudController : BaseCrudController<HomeWorkDto>
    {
        public HomeWorkCrudController(IHomeWorkService homeWorkService) : base(homeWorkService)
        { }
    }
}