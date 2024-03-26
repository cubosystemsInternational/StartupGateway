/**
 * Created by: Zuhri
 * Created on: 05/02/2024, 06/02/2024
 * Description: Projects class creation. DTO
 * 
 * */

/// <inheritdoc />
///<summary>
/// This method will return all investors
/// </summary>
/// <returns></returns>
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StartupGateway.BusinessEntities;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("projects")]
    public class Projects
	{
        [Key]
		public int Id {get; set;}
        [ForeignKey("companies")]
        public int CompanyId { get; set; }
        public required string ProjectName {get; set;}
        public required string ProjectTitle { get; set; }
        public required string ProjectDescription { get; set; }
        public double ProjectValuation { get; set; }
        public EntityStatus Status { get; set; }

        // Need to Add CreatedOn and CreatedBy
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

}