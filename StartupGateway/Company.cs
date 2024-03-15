using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.BusinessEntities
{
    [Table("companies")]
    public class Company
	{
        public int CompanyId { get; set; }
        public int IndustryId { get; set; }
        public string? CompanyName { get; set;}
        public string? Description { get; set;}
        public int Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

}

