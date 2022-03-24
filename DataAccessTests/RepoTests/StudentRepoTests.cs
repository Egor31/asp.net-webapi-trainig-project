using System.Linq;
using DataAccess.Models;
using DataAccess.Repos;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace DataAccessTests.RepoTests
{
    public class StudentRepoTests : BaseTest
    {
        private StudentRepo _studentRepo;

        [SetUp]
        public void StudentRepoSetup()
        {
            _studentRepo = new(_context, NullLogger<StudentRepo>.Instance);
        }

        [Test]
        public void GetStudentInStudentRepoReturnsStudent()
        {
            var actual = _studentRepo.Find(1);
            Assert.That(actual is not null);   
        }

        [Test]
        public void GetAllStudentsInStudentRepoReturnsStudents()
        {
            var expected = 5;
            var actual = _studentRepo.GetAll().Count();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AddStudentInStudentRepoAddsStudent()
        {
            var expected = new Student { FirstName = "Test" };
            _studentRepo.Add(expected);
            var actual = _studentRepo.GetAll()
                .Where(student => student.FirstName == "Test")
                .FirstOrDefault(); 
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateStudentInStudentRepoUpdatesStudent()
        {
            var expected = "John";
            var studentToUpdate = _studentRepo.Find(1);
            studentToUpdate.FirstName = expected;
            _studentRepo.Update(studentToUpdate);
            var actual = _studentRepo.Find(1).FirstName;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DeleteStudentByIdInStudentRepoDeletesStudent()
        {
            var id = 1;
            _studentRepo.Delete(id);
            var actual = _studentRepo.Find(id);
            Assert.That(actual is null);
        }

        [Test]
        public void DeleteStudentByEntityInStudentRepoDeletesStudent()
        {
            var id = 1;
            var entityToDelete = _studentRepo.Find(id);
            _studentRepo.Delete(entityToDelete);
            var actual = _studentRepo.Find(id);
            Assert.That(actual is null);
        }
    }
}