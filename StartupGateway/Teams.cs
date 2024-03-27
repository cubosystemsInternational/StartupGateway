﻿/**
 * Modified by: Ibrahim
 * Created on: 19/03/2024
 * Description: Team class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StartupGateway.Shared.Share;

namespace StartupGateway.Model
{
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("teams")]
    public class Teams
	{
        [Key]
        public int Id { get; set; }
		public required string TeamOwner { get; set; }
        public required string TeamName { get; set;}
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
