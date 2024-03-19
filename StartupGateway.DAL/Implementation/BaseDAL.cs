using Microsoft.EntityFrameworkCore;
using StartupGateway.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Implementation
{
    public class BaseDAL<TEntity> : IBaseDAL<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        public BaseDAL(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Add a new Record to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void AddEntity(TEntity entity)
        {
            _context.Add(entity);
        }
        /// <summary>
        /// Retrieve all Records from the database.
        /// </summary>
        /// <returns>A collection of all projects.</returns>
        public IEnumerable<TEntity> GetAllRecords()
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get a Record by its ID.
        /// </summary>
        /// <param name="projectId">The ID of the Record to retrieve.</param>
        /// <returns>The Record with the specified ID.</returns>
        public TEntity GetEntityById(int entityId)
        {
            return _context.Set<TEntity>().Find(entityId);
        }

        /// <summary>
        /// Get a Record by name using a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match the Record by name.</param>
        /// <returns>The Record that matches the predicate.</returns>
       
        public TEntity GetEntityByAttribute(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }
        /// <summary>
        /// Update an existing Record in the database.
        /// </summary>
        /// <param name="entity">The updated project entity.</param>

        public void UpdateEntity(TEntity entity)
        {
            if (_context.Set<TEntity>().Local.Any(e => e == entity))
            {
                _context.Set<TEntity>().Attach(entity);
            }

            // Mark the entity as modified to ensure it's updated in the database
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Dispose of the database context to free resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Commit changes to the database.
        /// </summary>
        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public List<TEntity> GetAllRecordsWithCondition(Func<TEntity, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
