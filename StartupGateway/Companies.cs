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
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("companies")]
    public class Companies
	{
        [Key]
        public int Id { get; set; }
        [ForeignKey("industry")]
        public int IndustryId { get; set; }
        public required string CompanyName { get; set;}
        public string? Description { get; set;}
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
   
}

