/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Interface repository class ProjectTeamsRepository created.
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
    public class ProjectTeamsRepository : BaseDAL<ProjectTeams>, IProjectTeamsRepository
    {
        public ProjectTeamsRepository(DataContext context) : base(context)
        {
        }
    }
}
