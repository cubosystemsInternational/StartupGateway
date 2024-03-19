using System;

namespace StartupGateway.UoW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 
        void Commit();
        void Rollback();
        T GetRepository<T>() where T : class;
    }
}
