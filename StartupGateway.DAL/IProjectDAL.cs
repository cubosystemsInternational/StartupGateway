using System;
namespace StartupGateway.DAL
{
	public interface IProjectDAL<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> GetAllProjects();
		public TEntity GetProjectById(int Projectid);
		public TEntity GetProjectByName(string ProjectName);

    }


}

