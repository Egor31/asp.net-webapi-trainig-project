using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DataAccess.Exceptions;
using DataAccess.Models;

namespace DataAccess.Repos.Base
{
    public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        public DbSet<T> Table { get; }
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BaseRepo<T>> _logger;

        public BaseRepo(ApplicationDbContext context, ILogger<BaseRepo<T>> logger)
        {
            _context = context;
            _logger = logger;
            Table = _context.Set<T>();
        }

        public virtual T Find(int id) => Table.Find(id);

        public virtual IEnumerable<T> GetAll() => Table;

        public virtual int Add(T entity)
        {
            Table.Add(entity);
            _logger.LogTrace($"Added entity of {typeof(T).Name}");
            return SaveChanges();
        }

        public virtual int Update(T entity)
        {
            Table.Update(entity);
            _logger.LogTrace($"Updated entity of {typeof(T).Name}");
            return SaveChanges();
        }

        public int Delete(int id)
        {
            // var entityToDelete = new T { Id = id }; // doesn't work when using sqlite in memory context in tests
            var entityToDelete = Find(id);
            _context.Entry(entityToDelete).State = EntityState.Deleted;
            _logger.LogTrace($"Deleted entity of {typeof(T).Name} with Id = {id}");
            return SaveChanges();
        }

        public virtual int Delete(T entity)
        {
            Table.Remove(entity);
            _logger.LogTrace($"Deleted entity of {typeof(T).Name}");
            return SaveChanges();
        }

        public int SaveChanges()
        {
            try
            {
                var saveChangesResult = _context.SaveChanges();
                _logger.LogTrace($"Changes of {typeof(T).Name} successfully saved with result {saveChangesResult}");
                return saveChangesResult;
            }
            catch (DbUpdateConcurrencyException dbConcurrencyException)
            {
                _logger.LogError(dbConcurrencyException, "Concurrency error saving to database");
                throw new DataAccessDbConcurrencyException("A concurrency error happened.", dbConcurrencyException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException, "Error saving to database");
                throw new DataAccessDbUpdateException("An error occurred updating the database", dbUpdateException);
            }
            catch (Exception justException)
            {
                _logger.LogError(justException, "Database error");
                throw new DataAccessDbException("An error occurred updating the database", justException);
            }
        }
    }
}
