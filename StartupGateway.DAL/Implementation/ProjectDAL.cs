/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  ProjectDAL [Data Access Layer] Class (Repository).
 * 
 * */

using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Implementation
{
    public class ProjectDAL : BaseDAL<Project>, IProjectDAL
    {
        public ProjectDAL(DataContext context) : base(context)
        {
        }
    }
}
