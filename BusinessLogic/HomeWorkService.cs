using System.Linq;
using BusinessLogic.MessageServices;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using DataAccess.Repos.Interfaces;
using FluentValidation;

namespace BusinessLogic
{
    public class HomeWorkService : BaseService<HomeWork, HomeWorkDto>, IHomeWorkService
    {
        private IStudentService _studentService;
        private ISubjectService _subjectService;
        private ISmsService _smsService;

        public HomeWorkService(
            IHomeWorkRepo homeWorkRepo,
            IValidator<HomeWorkDto> homeWorkValidator,
            IStudentService studentService,
            ISubjectService subjectService,
            ISmsService smsService) : base(homeWorkRepo, homeWorkValidator)
        {
            _studentService = studentService;
            _subjectService = subjectService;
            _smsService = smsService;
        }

        public override void CreateItem(HomeWorkDto homeWorkDto)
        {
            homeWorkDto.Id = 0;
            _entityValidator.ValidateAndThrow(homeWorkDto);
            var studentDto = _studentService.FindItem(homeWorkDto.StudentId);
            var subjectDto = _subjectService.FindItem(homeWorkDto.SubjectId);

            _dataRepo.Add(_mapper.Map<HomeWork>(homeWorkDto));

            SendSmsToStudentIfAverageGradeIsLessThanFour(studentDto, subjectDto, homeWorkDto);
        }

        private void SendSmsToStudentIfAverageGradeIsLessThanFour(
            StudentDto studentDto, 
            SubjectDto subjectDto, 
            HomeWorkDto homeWorkDto
        )
        {
            double averageStudentGradeOnCurrentSubject =
                _dataRepo.GetAll().Where(
                    homeWork => (
                        homeWork.SubjectId == homeWorkDto.SubjectId &&
                        homeWork.StudentId == homeWorkDto.StudentId
                    )
                ).Average(homeWork => homeWork.Grade);

            if (averageStudentGradeOnCurrentSubject < 4.0)
            {
                _smsService.SendMessage(
                    studentDto.PhoneNumber,
                    $"What issues do you have with {subjectDto.Title} class?"
                );
            }

        }
    }
}