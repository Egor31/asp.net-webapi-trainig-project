using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Exceptions
{
    class DataAccessDbUpdateException : DataAccessDbException
    {
        public DataAccessDbUpdateException() { }

        public DataAccessDbUpdateException(string message) : base(message) { }

        public DataAccessDbUpdateException(string message, DbUpdateException innerException)
            : base(message, innerException) { }
    }
}
