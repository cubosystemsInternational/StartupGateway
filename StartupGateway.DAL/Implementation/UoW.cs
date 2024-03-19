using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StartupGateway.DAL;

namespace StartupGateway.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Rollback changes if needed
        }

        public T GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (T)_repositories[typeof(T)];
            }

            T repository = (T)Activator.CreateInstance(typeof(T), _context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }
      
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
