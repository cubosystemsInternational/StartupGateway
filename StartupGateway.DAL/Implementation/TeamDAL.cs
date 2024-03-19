/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  TeamDAL [Data Access Layer] Class (Repository).
 * 
 * */

using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Implementation
{
    public class TeamDAL : BaseDAL<Team>, ITeamDAL
    {
        public TeamDAL(DataContext context) : base(context)
        {
        }
    }
}
