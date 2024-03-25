using System;

namespace StartupGateway.UoW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    { 
        void Commit();
        void Rollback();
        T GetDAL<T>() where T : class;
    }
}
