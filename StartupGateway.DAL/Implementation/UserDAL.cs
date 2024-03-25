/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  UserDAL [Data Access Layer] Class (Repository).
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
    public class UserDAL : BaseDAL<User>, IUserDAL
    {
        public UserDAL(DataContext context) : base(context)
        {

        }
    }
}
