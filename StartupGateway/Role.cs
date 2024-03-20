/**
 * Created by: Ibrahim
 * Created on: 18/03/2024
 * Description: Role class Model
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessEntities
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public RoleStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    // Used as a Temporary will need to change to a common share class
    public enum RoleStatus
    {
        Active,
        Inactive,
        Pending
    }
}
