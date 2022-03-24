using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos.Base
{
    public interface IRepo<T>
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(int id);
        int Delete(T entity);
        T Find(int id);
        IEnumerable<T> GetAll();
        int SaveChanges();
    }
}