/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Data Access class UserDocumentsDAL created.
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
    public class UserDocumentsDAL : BaseDAL<UserDocuments>, IUserDocumentsDAL
    {
        public UserDocumentsDAL(DataContext context) : base(context)
        {
        }
    }
}
