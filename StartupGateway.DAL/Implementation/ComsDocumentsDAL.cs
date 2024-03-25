/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Interface repository class ComsDocumentsRepository created.
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
    public class ComsDocumentsDAL : BaseDAL<ComsDocuments>, IComsDocumentsDAL
    {
        public ComsDocumentsDAL(DataContext context) : base(context)
        {
        }
    }
}
