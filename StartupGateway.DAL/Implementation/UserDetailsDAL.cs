/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Data Access class UserDetailsDAL created.
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
    public class UserDetailsDAL : BaseDAL<UserDetails>, IUserDetailsDAL
    {
        public UserDetailsDAL(DataContext context) : base(context)
        {
        }
    }
}
