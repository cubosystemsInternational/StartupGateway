/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description:  InvestorDAL [Data Access Layer] Class (Repository).
 * 
 * */

using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;

namespace StartupGateway.DAL.Implementation
{
    public class InvestorsDAL : BaseDAL<Investors>, IInvestorsDAL
    {
        public InvestorsDAL(DataContext context) : base(context)
        {
        }
    }
}
