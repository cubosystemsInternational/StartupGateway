/**
 * Modified by: Ibrahim
 * Created on: 19/03/2024
 * Description: Team class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("teams")]
    public class Team
	{
        [Key]
        public int TeamId { get; set; }
		public string? TeamOwner { get; set; }
        public string? TeamName { get; set;}
        public TeamStatus Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    // Used as a Temporary will need to change to a common share class
    public enum TeamStatus
    {
        Active,
        Inactive,
        Pending
    }
}

