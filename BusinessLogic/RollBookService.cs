using System;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using BusinessLogic.ModelsDto;
using Microsoft.Extensions.Logging;
using DataAccess.Repos.Interfaces;
using BusinessLogic.Mapping;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.MessageServices;

namespace BusinessLogic
{
    public class RollBookService : IRollBookService
    {
        private static IMapper _mapper = MapperDto.Service;
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;
        private readonly IHomeWorkService _homeWorkService;
        private readonly IRollBookRepo _rollBookRepo;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public RollBookService(
            IStudentService studentService,
            ISubjectService subjectService,
            ITeacherService teacherService,
            IHomeWorkService homeWorkService,
            IRollBookRepo rollBookRepo,
            IEmailService emailService,
            ILogger<RollBookService> logger)
        {
            _studentService = studentService;
            _subjectService = subjectService;
            _teacherService = teacherService;
            _homeWorkService = homeWorkService;
            _rollBookRepo = rollBookRepo;
            _emailService = emailService;
            _logger = logger;
        }

        public IEnumerable<RollBookRecordDto> GetRollBookRecordsBySubjectId(int subjectId)
        {
            var rollBookRecordsFromRepo = _rollBookRepo.GetAll().Where(record => record.SubjectId == subjectId);
            return rollBookRecordsFromRepo.Select(_mapper.Map<RollBookRecordDto>);
        }

        public IEnumerable<RollBookRecordDto> GetRollBookRecordsByStudentId(int studentId)
        {
            var rollBookRecordsFromRepo = _rollBookRepo.GetAll().Where(record => record.StudentId == studentId);
            return rollBookRecordsFromRepo.Select(_mapper.Map<RollBookRecordDto>);
        }

        public void CheckInStudent(int studentId, int subjectId, bool isStudentPresent)
        {
            var studentToCheckIn = _studentService.FindItem(studentId);
            var subjectCheckedIn = _subjectService.FindItem(subjectId);

            RollBookRecordDto newRollBookRecordDto = new()
            {
                Id = 0,
                StudentId = studentToCheckIn.Id,
                SubjectId = subjectCheckedIn.Id,
                Date = DateTime.Now,
                IsPresent = isStudentPresent
            };

            _rollBookRepo.Add(_mapper.Map<RollBookRecord>(newRollBookRecordDto));

            if (!isStudentPresent)
            {
                AddHomeWorkWithZeroGrade(studentId, subjectId);
            }
            
            var studentRollBookRecords = GetRollBookRecordsByStudentId(studentId);
            if (studentRollBookRecords.Where(record => 
                record.IsPresent == false && record.SubjectId == subjectId).Count() > 3)
            {
                var teacherToNotify = _teacherService.FindItem(subjectCheckedIn.TeacherId);
                _emailService.SendMessage(
                    teacherToNotify.Email, 
                    $"Student {studentToCheckIn.FirstName} {studentToCheckIn.LastName}" +
                    $" missed more than 3 {subjectCheckedIn.Title} classes"
                );
                _emailService.SendMessage(
                    studentToCheckIn.Email, 
                    $"Dear, {studentToCheckIn.FirstName} {studentToCheckIn.LastName}! " + 
                    $"You missed more than three {subjectCheckedIn.Title} classes"
                );
            }
        }

        private void AddHomeWorkWithZeroGrade(int studentId, int subjectId)
        {
            HomeWorkDto homeWorkWithZeroGradeDto = new()
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Grade = 0,
            };
            _homeWorkService.CreateItem(homeWorkWithZeroGradeDto);
        }
    }
}