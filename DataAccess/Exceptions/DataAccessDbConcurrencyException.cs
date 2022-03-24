using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Exceptions
{
    class DataAccessDbConcurrencyException : DataAccessDbException
    {
        public DataAccessDbConcurrencyException() { }

        public DataAccessDbConcurrencyException(string message) : base(message) { }

        public DataAccessDbConcurrencyException(string message, DbUpdateConcurrencyException innerException)
            : base(message, innerException) { }
    }
}
