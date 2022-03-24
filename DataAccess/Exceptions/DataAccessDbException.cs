using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Exceptions
{
    class DataAccessDbException : Exception
    {
        public DataAccessDbException() { }

        public DataAccessDbException(string message) : base(message) { }

        public DataAccessDbException(string message, Exception innerException) : base(message, innerException) { }
    }
}
