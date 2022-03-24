using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic;
using BusinessLogic.ServiceInterfaces;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RollBookServiceController : ControllerBase
    {
        private readonly IRollBookService _rollBookService;
        private readonly ILogger<RollBookServiceController> _logger;

        public RollBookServiceController(IRollBookService rollBookService, ILogger<RollBookServiceController> logger)
        {
            _rollBookService = rollBookService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult CheckInStudentPresenceInClass(
            int studentId,
            int subjectId,
            bool isStudentPresent)
        {
            _rollBookService.CheckInStudent(studentId, subjectId, isStudentPresent);
            return Ok(ApiResponse<object>.Success(null));
        }
    }
}
