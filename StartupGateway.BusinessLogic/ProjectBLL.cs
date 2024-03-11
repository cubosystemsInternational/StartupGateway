using System;
using StartupGateway.BusinessEntities;
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

        public ProjectBLL(ProjectDAL<Project> ProjectsDal)
		{
			this.projectsDal = ProjectsDal; 
		}

	}
}

