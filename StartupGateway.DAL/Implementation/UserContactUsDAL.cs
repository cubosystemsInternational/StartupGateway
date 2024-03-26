/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Data Access class UserContactUsDAL created.
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
    public class UserContactUsDAL: BaseDAL<UserContactUs>, IUserContactUsDAL
    {
        public UserContactUsDAL(DataContext context) : base(context) { }
    }
}
