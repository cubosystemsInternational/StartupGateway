/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: Collaborate class Model
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
    [Table("collaborate")]
    public class Collaborate
    {
        [Key]
        public int ComId { get; set; }

        [ForeignKey("projects")]
        public int? ProjectID { get; set; }
        [ForeignKey("teams")]
        public int? TeamId { get; set; }    
        public CollaborateStatus Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
    // Used as a Temporary will need to change to a common share class
    public enum CollaborateStatus
    {
        Active,
        Inactive,
        Pending
    }
}
