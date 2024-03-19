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
        public UserTeamStatus Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    // Used as a Temporary will need to change to a common share class
    public enum UserTeamStatus
    {
        Active,
        Inactive,
        Pending
    }
}
