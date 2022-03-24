using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using DataAccess.Repos.Base;
using DataAccess.Repos.Interfaces;

namespace DataAccess.Repos
{
    public class TeacherRepo : BaseRepo<Teacher>, ITeacherRepo
    {
        public TeacherRepo(ApplicationDbContext context, ILogger<TeacherRepo> logger) : base(context, logger)
        { }

        public override IEnumerable<Teacher> GetAll() =>
            Table
                .Include(teacher => teacher.Subjects)
                .OrderBy(teacher => teacher.FirstName);
    }
}