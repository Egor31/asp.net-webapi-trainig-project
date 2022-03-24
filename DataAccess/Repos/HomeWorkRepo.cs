using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Models;
using DataAccess.Repos.Base;
using DataAccess.Repos.Interfaces;

namespace DataAccess.Repos
{
    public class HomeWorkRepo : BaseRepo<HomeWork>, IHomeWorkRepo
    {
        public HomeWorkRepo(ApplicationDbContext context, ILogger<HomeWorkRepo> logger) : base(context, logger)
        { }

        public override HomeWork Find(int id) => 
            Table
                .Include(homeWork => homeWork.StudentNavigation)
                .Include(homeWork => homeWork.SubjectNavigation)
                .FirstOrDefault(homeWork => homeWork.Id == id);

        public override IEnumerable<HomeWork> GetAll() =>
            Table
                .Include(homeWork => homeWork.StudentNavigation)
                .Include(homeWork => homeWork.SubjectNavigation)
                .OrderBy(homeWork => homeWork.StudentNavigation.FirstName);
    }
}