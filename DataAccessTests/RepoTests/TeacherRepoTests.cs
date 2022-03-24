using System.Linq;
using DataAccess.Models;
using DataAccess.Repos;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace DataAccessTests.RepoTests
{
    public class TeacherRepoTests : BaseTest
    {
        private TeacherRepo _teacherRepo;

        [SetUp]
        public void TeacherRepoSetup()
        {
            _teacherRepo = new(_context, NullLogger<TeacherRepo>.Instance);
        }

        [Test]
        public void GetTeacherInTeacherRepoReturnsTeacher()
        {
            var actual = _teacherRepo.Find(1);
            Assert.That(actual is not null);   
        }

        [Test]
        public void GetTeacherInTeacherRepoReturnsTeacherWithSubjects()
        {
            var actual = _teacherRepo.Find(1);
            Assert.That(actual.Subjects is not null);
        }

        [Test]
        public void GetAllTeachersInTeacherRepoReturnsTeachers()
        {
            var expected = 3;
            var actual = _teacherRepo.GetAll().Count();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void AddTeacherInTeacherRepoAddsTeacher()
        {
            var teacherToAdd = new Teacher { FirstName = "Test" };
            var actual = _teacherRepo.Add(teacherToAdd);
            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void UpdateTeacherInTeacherRepoUpdatesTeacher()
        {
            var expected = "John";
            var teacherToUpdate = _teacherRepo.Find(1);
            teacherToUpdate.FirstName = expected;
            _teacherRepo.Update(teacherToUpdate);
            var actual = _teacherRepo.Find(1).FirstName;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DeleteTeacherByIdInTeacherRepoDeletesTeacher()
        {
            _teacherRepo.Delete(1);
            var actual = _teacherRepo.Find(1);
            Assert.That(actual is null);
        }

        [Test]
        public void DeleteTeacherByEntityInTeacherRepoDeletesTeacher()
        {
            var id = 1;
            var entityToDelete = _teacherRepo.Find(id);
            _teacherRepo.Delete(entityToDelete);
            var actual = _teacherRepo.Find(1);
            Assert.That(actual is null);
        }
    }
}