using System;
namespace StartupGateway.DAL
{
	public interface IProjectDAL<TEntity> where TEntity : class
	{
		public void AddEntity(TEntity entity);

        IEnumerable<TEntity> GetAllProjects();
		public TEntity GetProjectById(int Projectid);
		public TEntity GetProjectByName(Func<TEntity, bool> predicate);
		public void UpdateProject(TEntity entity);

        public void CommitChanges();



    }


}

