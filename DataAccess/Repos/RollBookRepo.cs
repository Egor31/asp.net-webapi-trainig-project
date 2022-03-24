using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Models;
using DataAccess.Repos.Base;
using DataAccess.Repos.Interfaces;

namespace DataAccess.Repos
{
    public class RollBookRepo : BaseRepo<RollBookRecord>, IRollBookRepo 
    {
        public RollBookRepo(ApplicationDbContext context, ILogger<RollBookRepo> logger) : base(context, logger)
        { }

        public override IEnumerable<RollBookRecord> GetAll() =>
            Table
                .Include(rollBookRecord => rollBookRecord.StudentNavigation)
                .Include(rollBookRecord => rollBookRecord.SubjectNavigation)
                .OrderBy(rollBookRecord => rollBookRecord.Date);
    }
}