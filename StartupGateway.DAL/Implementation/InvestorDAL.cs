/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  InvestorDAL [Data Access Layer] Class (Repository).
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
    public class InvestorDAL : BaseDAL<Investor>, IInvestorDAL
    {
        public InvestorDAL(DataContext context) : base(context)
        {
        }
    }
}
