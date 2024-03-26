/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Data access class CommunicationsDocumentsDAL created.
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
    public class CommunicationDocumentsDAL : BaseDAL<CommunicationDocuments>, ICommunicationDocumentsDAL
    {
        public CommunicationDocumentsDAL(DataContext context) : base(context)
        {
        }
    }
}
