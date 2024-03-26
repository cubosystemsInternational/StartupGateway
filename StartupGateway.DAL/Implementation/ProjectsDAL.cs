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
    public class ProjectsDAL : BaseDAL<Projects>, IProjectsDAL
    {
        public ProjectsDAL(DataContext context) : base(context)
        {
        }
    }
}
