/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Data access class DeviceTypeDAL created.
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
    internal class DeviceTypesDAL : BaseDAL<DeviceTypes>, IDeviceTypesDAL
    {
        public DeviceTypesDAL(DataContext context) : base(context)
        {
        }
    }
}
