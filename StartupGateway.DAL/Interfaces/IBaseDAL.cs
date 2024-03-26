/**
 * Created by: Zaid
 * Created on: 19/03/2024
 * Description: Interface IBasedDAL (Repository).
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Interfaces
{
    public interface IBaseDAL<TEntity>:IDisposable where TEntity : class
    {

        /// <summary>
        /// Retrieve all Records from the database.
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> GetAllRecords();

        /// <summary>
        /// Get a Record by its ID.
        /// values currently not nullable.
        /// </summary>
        /// <param name="entityId">The ID of the Record to retrieve.</param>
        /// <returns>Instance of TEntity?</returns>
        TEntity? GetEntityById(int entityId);

        /// <summary>
        /// Add a new Record to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void AddEntity(TEntity entity);

        /// <summary>
        /// Get a Record by Attribute using a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match the Record by name.</param>
        /// <returns>Instance of TEntity?</returns>
        TEntity? GetEntityByAttribute(Func<TEntity, bool> predicate);

        /// <summary>
        /// Retrieve all Records from the database with condition.
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> GetAllRecordsWithCondition(Func<TEntity,bool> predicate) ;

        /// <summary>
        /// Finds a collection of values based on the condition passed.
        /// This performance function does not keep track of changes.
        /// Utilize only for retrieving information and not updating.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable of <c>TEntity</c></returns>
        IEnumerable<TEntity> FindByConditionPerformance(Func<TEntity, bool> predicate);

        /// <summary>
        /// Update an existing Record in the database.
        /// </summary>
        /// <param name="entity">The updated project entity.</param>
        void UpdateEntity(TEntity entity);

        /// <summary>
        /// Commit changes to the database.
        /// </summary>
        void CommitChanges();
    }
}
