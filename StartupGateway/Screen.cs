/**
 * Modified by: Ibrahim
 * Created on: 19/03/2024
 * Description: Screen class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StartupGateway.Shared.Share;

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
		public required string ScreenName { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

}

