using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Interfaces
{
    public interface IBaseDAL<TEntity>:IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAllRecords();
        TEntity GetEntityById(int id);
        void AddEntity(TEntity entity);
        TEntity GetEntityByAttribute(Func<TEntity, bool> predicate);
        List<TEntity> GetAllRecordsWithCondition(Func<TEntity,bool> predicate) ;

        void UpdateEntity(TEntity entity);
        void CommitChanges();
    }
}
