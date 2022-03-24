using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Models;
using DataAccess.Repos.Base;
using DataAccess.Repos.Interfaces;

namespace DataAccess.Repos
{
    public class StudentRepo : BaseRepo<Student>, IStudentRepo
    {
        public StudentRepo(ApplicationDbContext context, ILogger<StudentRepo> logger) : base(context, logger)
        { }

        public override IEnumerable<Student> GetAll() =>
            Table
                .Include(student => student.HomeWorks)
                .OrderBy(student => student.FirstName);
    }
}