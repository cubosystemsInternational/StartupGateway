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
	public class Project
	{
        [Key]
		public int Projectid {get; set;}
        //[ForeignKey]
        public Company CompanyId { get; set; }
        public string ProjectName {get; set;}
        public string ProjectTitle { get; set; }
        public enum Status {}
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        
    }
}

//byall
//byid
//byname
