/**
 * Created by: Ibrahim
 * Created on: 20/03/2024
 * Description: Share Class with The common Enumeration Type and Other Common Shareable
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.Shared
{
    public static class Share
    {
        /// <summary>
        /// This is  The common Enumeration Type used in most of the Models (Entities)
        /// <return> Active = 0 ,Inactive = 1,Pending = 3</return>
        /// </summary>
        public enum EntityStatus
        {
            Active,
            Inactive,
            Pending
        }
    }
}
