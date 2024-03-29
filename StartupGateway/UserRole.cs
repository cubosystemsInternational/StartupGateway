﻿/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: UserRole class Model
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    [Table("userRoles")]
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        
        [ForeignKey("roles")]
        public int RoleId { get; set; }
        [ForeignKey("users")]
        public int UserId { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
