/**
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
    [Table("teams")]
    public class Team
	{
        [Key]
        public int TeamId { get; set; }
		public required string TeamOwner { get; set; }
        public required string TeamName { get; set;}
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}

