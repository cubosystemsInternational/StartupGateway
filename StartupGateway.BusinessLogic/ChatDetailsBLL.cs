/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ChatDetailsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using Mysqlx;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing chat details.
    /// </summary>
    public class ChatDetailsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly UnitOfWork unitOfWork;
        //priva

        /// <summary>
        /// Constructor to intialize ChatDetailsBLL with necessary dependencies.
        /// </summary>
        public ChatDetailsBLL(ILogger<ChatDetailsBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
    }
}
