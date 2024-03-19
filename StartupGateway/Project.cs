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

namespace StartupGateway.BusinessEntities
{
    [Table("projects")]
    public class Project
	{
        [Key]
		public int ProjectId {get; set;}
        [ForeignKey("company")]
        public int? CompanyId { get; set; }
        public string? ProjectName {get; set;}
        public string? ProjectTitle { get; set; }
        public string? ProjectDescription { get; set; }
        public double? ProjectValuation { get; set; }
        public ProjectStatus Status { get; set; }

        // Need to Add CreatedOn and CreatedBy
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }

    public enum ProjectStatus
    {
        Active,
        Inactive,
        Pending
    }
}

//byall
//byid
//byname
