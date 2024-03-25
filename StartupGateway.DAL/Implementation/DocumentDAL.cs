/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  DocumentDAL [Data Access Layer] Class (Repository).
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
    public class DocumentDAL : BaseDAL<Document>, IDocumentDAL
    {
        public DocumentDAL(DataContext context) : base(context)
        {
        }
    }
}
