/**
 * Modified by: Ibrahim
 * Created on: 19/03/2024
 * Description: Screen class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("screens")]

    public class Screen
	{
        [Key]
        public int ScreenId { get; set; }
        [ForeignKey("projects")]
        public int ProjectId { get; set; }
        [ForeignKey("teams")]
        public int TeamId { get; set; }
		public string? ScreenName { get; set; }
        public ScreenStatus Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }

    // Used as a Temporary will need to change to a common share class
    public enum ScreenStatus
    {
        Active,
        Inactive,
        Pending
    }
}

