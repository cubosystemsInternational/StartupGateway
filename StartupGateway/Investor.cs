/**
 * Created by: Zaid
 * Modified by : Ibrahim
 * Created on: 18/03/2024
 * Description: Investor class Model
 * 
 * */
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("investors")]

    public class Investor
	{
		public int InvestorsId { get; set; }
        [ForeignKey("user")]
        public int UserId { get; set; }
		public double InvestmentValue { get; set; }
		public bool Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}

