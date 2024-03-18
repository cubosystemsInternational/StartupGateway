using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("screens")]
    public class Teams
	{

		public int TeamId { get; set; }
		public string? TeamOwner { get; set; }
        public string? TeamName { get; set;}
        public int Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}

