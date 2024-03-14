﻿using StartupGateway.Model;

namespace StartupGateway.DAL;
public class ProjectDAL<TEntity> : IProjectDAL<TEntity> where TEntity : class
{
    private readonly DataContext _context;
    public ProjectDAL(DataContext context)
    {
        _context = context;
    }
    public void AddEntity(TEntity entity)
    {
        _context.Add(entity);
    }

    /// <inheritdoc />
    ///<summary>
    /// This method will return all Projects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TEntity> GetAllProjects()
    {
        // Retrieve all entities of the specified type
        return _context.Set<TEntity>().ToList();
    }
    /// <inheritdoc />
    ///<summary>
    /// This method will return a project by id
    /// </summary>
    /// <returns></returns>
    public TEntity GetProjectById(int Projectid)
    {
        // Retrieve the entity by its primary key
        return _context.Set<TEntity>().Find(Projectid);
    }
    /// <inheritdoc />
    ///<summary>
    /// This method will return a project by name
    /// </summary>
    /// <returns></returns>
    public TEntity GetProjectByName(Func<TEntity, bool> predicate)
    {
        return _context.Set<TEntity>().FirstOrDefault(predicate);
    }
    public void Dispose()
    {
        _context.Dispose();
    }

    public void CommitChanges()
    {
        _context.SaveChanges();
    }


}


//Scripts to write to the database (Insert, Update, Delete), ORM or repository patterns.