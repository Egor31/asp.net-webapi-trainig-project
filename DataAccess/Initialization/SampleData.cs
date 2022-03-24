using System;
using System.Collections.Generic;
using DataAccess.Models;

namespace DataAccess.Initialization
{
    public static class SampleData
    {
        const string fakePhoneNumber = "+1(202)358-0001";
        const string fakeEmail = "fakeemail@studentscrm.com";

        public static List<Student> Students => 
        new()
        {
            new() { FirstName = "David", LastName = "King", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Jake", LastName = "Park", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Nea", LastName = "Karlsson", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Quentin", LastName = "Smith", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Felix", LastName = "Richter", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
        };

        public static List<Teacher> Teachers =>
        new()
        {
            new() { FirstName = "Evan", LastName = "MacMillan", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Philip", LastName = "Ojomo", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
            new() { FirstName = "Max", LastName = "Thompson Jr.", Email = fakeEmail, PhoneNumber = fakePhoneNumber },
        };

        public static List<Subject> Subjects =>
        new()
        {
            new() { Title = "Farming", TeacherId = 3 },
            new() { Title = "Architecture", TeacherId = 1},
            new() { Title = "Automotive", TeacherId = 2},
        };

        public static List<HomeWork> HomeWorks =>
        new()
        {
            new() { StudentId = 1, SubjectId = 1, Grade = 4 },
            new() { StudentId = 2, SubjectId = 2, Grade = 5 },
            new() { StudentId = 3, SubjectId = 3, Grade = 4 },
            new() { StudentId = 4, SubjectId = 1, Grade = 5 },
            new() { StudentId = 5, SubjectId = 2, Grade = 4 },
            new() { StudentId = 1, SubjectId = 2, Grade = 0 },
        };

        public static List<RollBookRecord> RollBook =>
        new()
        {
            new() { StudentId = 1, SubjectId = 1, Date = new DateTime(2016, 6, 14), IsPresent = true },
            new() { StudentId = 2, SubjectId = 2, Date = new DateTime(2016, 6, 15), IsPresent = true },
            new() { StudentId = 3, SubjectId = 3, Date = new DateTime(2016, 6, 16), IsPresent = true },
            new() { StudentId = 4, SubjectId = 1, Date = new DateTime(2016, 6, 14), IsPresent = true },
            new() { StudentId = 5, SubjectId = 2, Date = new DateTime(2016, 6, 15), IsPresent = true },
            new() { StudentId = 1, SubjectId = 2, Date = new DateTime(2016, 6, 17), IsPresent = false },
        };
    }
}