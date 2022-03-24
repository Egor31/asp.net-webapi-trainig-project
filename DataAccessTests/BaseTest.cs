using DataAccess;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace DataAccessTests
{
    public abstract class BaseTest
    {
        protected InMemoryDbConextFactory _factory;
        protected ApplicationDbContext _context;
        

        [SetUp]
        public void ContextSetup()
        {
            _factory = new InMemoryDbConextFactory();
            _context = _factory.CreateContext();
        }

        [TearDown]
        public void FactoryDispose()
        {
            _factory.Dispose();
        }
    }
}