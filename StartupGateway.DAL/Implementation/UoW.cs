using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StartupGateway.DAL;
using StartupGateway.UoW.Interfaces;

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

        //public T GetDAL<T>() where T : class
        //{
        //    if (_repositories.ContainsKey(typeof(T)))
        //    {
        //        return (T)_repositories[typeof(T)];
        //    }

        //    T repository = Activator.CreateInstance(typeof(T), _context) as T;
        //    _repositories.Add(typeof(T), repository);
        //    return repository;
        //}
        public T GetDAL<T>() where T : class
        {
            Type interfaceType = typeof(T);

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException("T must be an interface");
            }

            Type repositoryType = Assembly.GetExecutingAssembly()
                                          .GetTypes()
                                          .FirstOrDefault(t => t.IsClass && t.GetInterfaces().Contains(interfaceType));

            if (repositoryType == null)
            {
                throw new InvalidOperationException($"No concrete implementation found for interface {interfaceType.Name}");
            }

            var repository = (T)Activator.CreateInstance(repositoryType, _context);

            return repository;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
