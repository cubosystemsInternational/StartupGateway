/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: Chat class Model
 * 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    [Table("chats")]
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public required string ChatTitle { get; set; }
        public required string ChatBody { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
