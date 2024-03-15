using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("screens")]

    public class Screens
	{
		public int ScreenId { get; set; }
		public int ProjectId { get; set; }
		public int TeamId { get; set; }
		public int ScreenName { get; set; }
        public int Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}

