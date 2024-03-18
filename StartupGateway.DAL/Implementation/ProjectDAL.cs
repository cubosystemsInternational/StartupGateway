using Microsoft.EntityFrameworkCore;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupGateway.DAL.Implementation
{
    /// <summary>
    /// Data access layer for managing projects using Entity Framework Core.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity (e.g., Project).</typeparam>
    public class ProjectDAL<TEntity> : IProjectDAL<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        /// <summary>
        /// Constructor to initialize ProjectDAL with the database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProjectDAL(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void AddEntity(TEntity entity)
        {
            _context.Add(entity);
        }

        /// <summary>
        /// Retrieve all projects from the database.
        /// </summary>
        /// <returns>A collection of all projects.</returns>
        public IEnumerable<TEntity> GetAllProjects()
        {
            // Retrieve all entities of the specified type
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Get a project by its ID.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve.</param>
        /// <returns>The project with the specified ID.</returns>
        public TEntity GetProjectById(int ProjectId)
        {
            // Retrieve the entity by its primary key
            return _context.Set<TEntity>().Find(ProjectId);
        }

        /// <summary>
        /// Get a project by name using a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match the project by name.</param>
        /// <returns>The project that matches the predicate.</returns>
        public TEntity GetProjectByName(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Update an existing project in the database.
        /// </summary>
        /// <param name="entity">The updated project entity.</param>
        public void UpdateProject(TEntity entity)
        {
            // Attach the entity to the context if it's not already tracked
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
    }
}
