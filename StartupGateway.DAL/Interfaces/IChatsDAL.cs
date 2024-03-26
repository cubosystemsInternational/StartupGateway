/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: Interface IChatDAL (Repository).
 * 
 * */

using StartupGateway.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.DAL.Interfaces
{
    public interface IChatsDAL: IBaseDAL<Chats>
    {
    }
}
