using BusinessLogic;
using BusinessLogic.ModelsDto;
using DataAccess.Repos;
using DataAccess.Models;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Repos.Interfaces;
using BusinessLogic.MessageServices;
using System;

namespace BusinessLogicTests
{
    public class RollBookServiceTests
    {
        Mock<IStudentService> studentServiceMock;
        Mock<ISubjectService> subjectServiceMock;
        Mock<ITeacherService> teacherServiceMock;
        Mock<IHomeWorkService> homeWorkServiceMock;
        Mock<IRollBookRepo> rollBookRepoMock;
        Mock<IEmailService> emailServiceMock;
        Mock<ILogger<RollBookService>> loggerMock;

        RollBookService rollBookServiceUnderTest;

        [SetUp]
        public void CreateMocksAndRollBookServiceUnderTest()
        {
            studentServiceMock = new();
            studentServiceMock
                .Setup(service => service.FindItem(It.IsAny<int>()))
                .Returns(
                    new StudentDto 
                    { 
                        Id = 1,
                        FirstName = "Test",
                        LastName = "Test",
                        Email = "student@test.edu",
                    });

            subjectServiceMock = new();
            subjectServiceMock
                .Setup(service => service.FindItem(It.IsAny<int>()))
                .Returns(new SubjectDto { Id = 1, Title = "Test" });

            teacherServiceMock = new();
            teacherServiceMock
                .Setup(service => service.FindItem(It.IsAny<int>()))
                .Returns(new TeacherDto { Id = 1, Email = "teacher@test.edu"});

            homeWorkServiceMock = new();
            homeWorkServiceMock.Setup(service => service.CreateItem(It.IsAny<HomeWorkDto>()));

            rollBookRepoMock = new();
            rollBookRepoMock
                .Setup(repo => repo.Add(It.IsAny<RollBookRecord>()));
            RollBookRecord[] rollBook = 
            {
                new() { StudentId = 1, SubjectId = 1, Date = new DateTime(2016, 6, 14), IsPresent = false },
                new() { StudentId = 1, SubjectId = 1, Date = new DateTime(2016, 6, 16), IsPresent = false },
                new() { StudentId = 1, SubjectId = 1, Date = new DateTime(2016, 6, 17), IsPresent = false },
                new() { StudentId = 1, SubjectId = 1, Date = new DateTime(2016, 6, 18), IsPresent = false },
                new() { StudentId = 1, SubjectId = 2, Date = new DateTime(2016, 6, 15), IsPresent = true },
                new() { StudentId = 1, SubjectId = 2, Date = new DateTime(2016, 6, 17), IsPresent = false },
                new() { StudentId = 1, SubjectId = 4, Date = new DateTime(2016, 6, 18), IsPresent = false },
            };
            rollBookRepoMock.Setup(repo => repo.GetAll()).Returns(rollBook);

            emailServiceMock = new();
            emailServiceMock.Setup(service => service.SendMessage(It.IsAny<string>(), It.IsAny<string>()));

            loggerMock = new();

            rollBookServiceUnderTest = new (
                studentServiceMock.Object,
                subjectServiceMock.Object,
                teacherServiceMock.Object,
                homeWorkServiceMock.Object,
                rollBookRepoMock.Object,
                emailServiceMock.Object,
                loggerMock.Object
            );
        }

        [Test]
        public void CheckInPresentStudentAddsRollBookRecordAndDoNotAddsHomeWork()
        {
            var studentAttendanceState = true;
            rollBookServiceUnderTest.CheckInStudent(1, 1, studentAttendanceState);

            rollBookRepoMock.Verify(
                repo => repo.Add(It.Is<RollBookRecord>(record => record.IsPresent == studentAttendanceState)), 
                Times.Once()
            );

            homeWorkServiceMock.Verify(
                service => service.CreateItem(It.IsAny<HomeWorkDto>()), 
                Times.Never()
            );
        }

        [Test]
        public void CheckInAbsentStudentAddsRollBookRecordAndAddsHomeWork()
        {
            var studentAttendanceState = false;
            rollBookServiceUnderTest.CheckInStudent(1, 1, studentAttendanceState);

            rollBookRepoMock.Verify(
                repo => repo.Add(It.Is<RollBookRecord>(record => record.IsPresent == studentAttendanceState)), 
                Times.Once()
            );

            homeWorkServiceMock.Verify(
                service => service.CreateItem(It.Is<HomeWorkDto>(hw => hw.Grade == 0)), 
                Times.Once()
            );
        }

        [Test]
        public void CheckInAbsentStudentMoreThanThreeTimesEmailsGetsSend()
        {
            var studentAttendanceState = false;
            rollBookServiceUnderTest.CheckInStudent(1, 1, studentAttendanceState);

            emailServiceMock.Verify(
                service => service.SendMessage(
                    It.Is<string>(emailAddress => emailAddress == "student@test.edu"),
                    It.Is<string>(
                        message => message == "Dear, Test Test! You missed more than three Test classes"
                    )
                ),
                Times.Once()
            );

            emailServiceMock.Verify(
                service => service.SendMessage(
                    It.Is<string>(emailAddress => emailAddress == "teacher@test.edu"),
                    It.Is<string>(
                        message => message == "Student Test Test missed more than 3 Test classes"
                    )
                ),
                Times.Once()
            );
        }
    }
}