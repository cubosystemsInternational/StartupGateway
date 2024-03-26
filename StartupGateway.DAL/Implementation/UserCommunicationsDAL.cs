/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Data access class UserCommunicationsDAL created.
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
    public class UserCommunicationsDAL : BaseDAL<UserCommunications>, IUserCommunicationsDAL
    {
        public UserCommunicationsDAL(DataContext context) : base(context)
        {
        }
    }
}
