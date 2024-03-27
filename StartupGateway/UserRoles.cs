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
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("user_roles")]
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("roles")]
        public int RoleId { get; set; }
        [ForeignKey("users")]
        public int UserId { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}