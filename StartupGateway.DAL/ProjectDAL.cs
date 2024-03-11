using StartupGateway.Model;

namespace StartupGateway.DAL;
public class ProjectDAL<TEntity> : IProjectDAL<TEntity> where TEntity : class
{
    //
    private readonly DataContext _context;
    public ProjectDAL(DataContext context)
    {
        _context = context;
    }
    /// <inheritdoc />
    ///<summary>
    /// This method will return all users
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TEntity> GetAllProjects()
    {
        // Retrieve all entities of the specified type
        return _context.Set<TEntity>().ToList();
    }
    public TEntity GetProjectById(int Projectid)
    {
        // Retrieve the entity by its primary key
        return _context.Set<TEntity>().Find(Projectid);
    }
    public TEntity GetProjectByName(string ProjectName)
    {
        // Retrieve the entity by its Name
        return _context.Set<TEntity>().Find(ProjectName);
    }

}


//Scripts to write to the database (Insert, Update, Delete), ORM or repository patterns.