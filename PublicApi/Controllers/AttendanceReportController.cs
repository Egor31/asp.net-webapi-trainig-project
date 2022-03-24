using System.Collections.Generic;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [FormatFilter]
    public class AttendanceReportController : ControllerBase
    {
        private IRollBookService _rollBookService;

        public AttendanceReportController(IRollBookService rollBookService)
        {
            _rollBookService = rollBookService;
        }

        [HttpGet("student{studentId}.{format?}")]
        public IEnumerable<RollBookRecordDto> GetReportByStudentId(int studentId)
        {
            return _rollBookService.GetRollBookRecordsByStudentId(studentId);
        }

        [HttpGet("subject{subjectId}.{format?}")]
        public IEnumerable<RollBookRecordDto> GetReportBySubjectId(int subjectId)
        {
            return _rollBookService.GetRollBookRecordsBySubjectId(subjectId);
        }
    }
}