using BusinessLogic;
using BusinessLogic.MessageServices;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using DataAccess.Models;
using DataAccess.Repos.Interfaces;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace BusinessLogicTests
{
    public class HomeWorkServiceTests
    {
        Mock<IHomeWorkRepo> homeWorkRepoMock;
        Mock<IValidator<HomeWorkDto>> homeWorkValidatorMock;
        Mock<IStudentService> studentServiceMock;
        Mock<ISubjectService> subjectServiceMock;
        Mock<ISmsService> smsServiceMock;

        HomeWorkService homeWorkServiceUnderTest;

        [SetUp]
        public void CreateMocks()
        {
            homeWorkRepoMock = new();
            homeWorkRepoMock.Setup(repo => repo.Add(It.IsAny<HomeWork>())).Returns(1);
            HomeWork[] homeWorks =
            {
                new() { StudentId = 1, SubjectId = 1, Grade = 4 },
                new() { StudentId = 1, SubjectId = 1, Grade = 4 },
                new() { StudentId = 1, SubjectId = 1, Grade = 4 },
                new() { StudentId = 1, SubjectId = 1, Grade = 3 },
            };
            homeWorkRepoMock.Setup(repo => repo.GetAll()).Returns(homeWorks);

            homeWorkValidatorMock = new();
            // ValidateAndThrow is an extension method so mocking it doesn't work, 
            // instead you have to somehow dig deep in FluentValidation code and found out which method called under the hood 
            // but StackOverflow already knows it so do not worry and just copy and paste
            homeWorkValidatorMock.Setup(validator => validator.Validate(It.IsAny<ValidationContext<HomeWorkDto>>()));

            studentServiceMock = new();
            studentServiceMock
                .Setup(service => service.FindItem(It.IsAny<int>()))
                .Returns(new StudentDto() {Id = 1, PhoneNumber = "+1(202)358-0001"});

            subjectServiceMock = new();
            subjectServiceMock
                .Setup(service => service.FindItem(It.IsAny<int>()))
                .Returns(new SubjectDto() { Id = 1, Title = "Test"});

            smsServiceMock = new();
            smsServiceMock.Setup(service => service.SendMessage(It.IsAny<string>(), It.IsAny<string>()));

            homeWorkServiceUnderTest = new(
                homeWorkRepoMock.Object,
                homeWorkValidatorMock.Object,
                studentServiceMock.Object,
                subjectServiceMock.Object,
                smsServiceMock.Object
            );
        }

        [Test]
        public void CreateItemWithHomeWorkDtoRepoAddsHomeWork()
        {
            homeWorkServiceUnderTest.CreateItem(
                new HomeWorkDto() { Id = 1, StudentId = 1, SubjectId = 1, Grade = 4 }
            );
            
            homeWorkRepoMock.Verify(
                repo => repo.Add(
                    It.Is<HomeWork>(
                        hw => 
                        hw.StudentId == 1 && 
                        hw.SubjectId == 1 && 
                        hw.Grade == 4
                    )
                ), 
                Times.Once()
            );
        }

        [Test]
        public void CreateItemWithHomeWorkDtoLowGradeSmsServiceSendsMessageToStudentAskingAboutIssuesWithSubject()
        {
            homeWorkServiceUnderTest.CreateItem(new HomeWorkDto() { Id = 1, StudentId = 1, SubjectId = 1, Grade = 3 });

            smsServiceMock.Verify(
                service => service.SendMessage(
                    It.Is<string>(phoneNumber => phoneNumber == "+1(202)358-0001"),
                    It.Is<string>(
                        message => message == "What issues do you have with Test class?"
                    )
                ),
                Times.Once()
            );
        }
    }
}