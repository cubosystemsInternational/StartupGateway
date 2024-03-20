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
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public required string Description { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

}
