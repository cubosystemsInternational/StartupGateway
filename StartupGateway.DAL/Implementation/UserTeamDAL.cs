/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  UserTeamDAL [Data Access Layer] Class (Repository).
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
    public class UserTeamDAL : BaseDAL<UserTeam>, IUserTeamDAL
    {
        public UserTeamDAL(DataContext context) : base(context)
        {
        }
    }
}
