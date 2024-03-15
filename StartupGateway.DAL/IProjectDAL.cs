using System;
using System.Collections.Generic;

namespace StartupGateway.DAL
{
    /// <summary>
    /// Interface for project data access layer.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity (e.g., Project).</typeparam>
    public interface IProjectDAL<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public void AddEntity(TEntity entity);

        /// <summary>
        /// Get all projects from the database.
        /// </summary>
        /// <returns>A collection of all projects.</returns>
        IEnumerable<TEntity> GetAllProjects();

        /// <summary>
        /// Get a project by its ID.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve.</param>
        /// <returns>The project with the specified ID.</returns>
        public TEntity GetProjectById(int Projectid);

        /// <summary>
        /// Get a project by its name using a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match the project by name.</param>
        /// <returns>The project that matches the predicate.</returns>
        public TEntity GetProjectByName(Func<TEntity, bool> predicate);

        /// <summary>
        /// Update an existing project in the database.
        /// </summary>
        /// <param name="entity">The updated project entity.</param>
        public void UpdateProject(TEntity entity);

        /// <summary>
        /// Commit changes to the database.
        /// </summary>
        public void CommitChanges();
    }
}
