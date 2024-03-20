/**
 * Created by : Zaid
 * Modified by: Ibrahim
 * Created on: 18/03/2024
 * Description: Company class Model
 * 
 * */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.BusinessEntities
{
    [Table("companies")]
    public class Company
	{
        [Key]
        public int CompanyId { get; set; }
        [ForeignKey("industry")]
        public int IndustryId { get; set; }
        public string CompanyName { get; set;}
        public string? Description { get; set;}
        public CompanyStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
    // Used as a Temporary will need to change to a common share class
    public enum CompanyStatus
    {
        Active,
        Inactive,
        Pending
    }
}

