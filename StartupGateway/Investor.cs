using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("investors")]

    public class Investors
	{
		public int InvestorsId { get; set; }
		public int UserId { get; set; }
		public double InvestmentValue { get; set; }
		public bool Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

