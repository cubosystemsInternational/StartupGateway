/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: Bid class Model
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
    [Table("userTeams")]
    public class UserTeam
    {
        [Key]
        public int UserTeamId { get; set; }
        [ForeignKey("teams")]
        public int TeamId { get; set; }
        [ForeignKey("users")]
        public int UserId { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
