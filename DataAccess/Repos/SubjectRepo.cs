using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Models;
using DataAccess.Repos.Base;
using DataAccess.Repos.Interfaces;

namespace DataAccess.Repos
{
    public class SubjectRepo : BaseRepo<Subject>, ISubjectRepo
    {
        public SubjectRepo(ApplicationDbContext context, ILogger<SubjectRepo> logger) : base(context, logger)
        { }

        public override IEnumerable<Subject> GetAll() =>
            Table
                .Include(subject => subject.TeacherNavigation)
                .OrderBy(subject => subject.Title);
    }
}