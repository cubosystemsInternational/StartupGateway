﻿/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class UserDetailsBLL created.
 * 
 * */


using Microsoft.Extensions.Logging;
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model UserDetailsBLL.
    /// </summary>
    public class UserDetailsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize UserDetailsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public UserDetailsBLL(ILogger<ChatDetailsBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
    }
}
