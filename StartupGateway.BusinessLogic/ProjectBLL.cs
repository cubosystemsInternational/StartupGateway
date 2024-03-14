using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using StartupGateway.BusinessEntities;
using StartupGateway.BusinessEntities.ReqModels;
using StartupGateway.DAL;
using StartupGateway.Model;
/***
* Created by: Zuhri and Zaid
* Created on: 06/02/2024
* Description: Projects methods creation. BLL
* 
**/

namespace StartupGateway.BusinessLogic
{
	public class ProjectBLL
	{

		private readonly IProjectDAL<Project> projectsDal;

        public ProjectBLL(IProjectDAL<Project> ProjectsDal)
		{
			this.projectsDal = ProjectsDal; 
		}
        /// <inheritdoc />
        ///<summary>
        /// This method has used GetProjectById from ProjectDal for BL purposes 
        /// </summary>
        /// <returns></returns>
        public Project GetProjectById(int projectId) {

			return projectsDal.GetProjectById(projectId);
		}
        /// <inheritdoc />
        ///<summary>
        /// This method has used GetProjectByName from ProjectDal for BL purposes 
        /// </summary>
        /// <returns></returns>
        public Project GetProjectByName(string projectName)
        {

            return projectsDal.GetProjectByName(x=> x.ProjectName == projectName);
        }
        /// <inheritdoc />
        ///<summary>
        /// This method has used GetAllProjects from ProjectDal for BLL purposes and returns a list of projects in the database
        /// </summary>
        /// <returns></returns>
        public List<Project> GetAllProjects()
        {

            return (List<Project>)projectsDal.GetAllProjects();
        }

        public void AddProject(CreateProjectModel project) {

            projectsDal.AddEntity(new Project
            {
                ProjectName = project.ProjectName,
                ProjectTitle = project.ProjectTitle,
                ProjectDesc = project.ProjectDescription,
            });
        
        }
    }
}

