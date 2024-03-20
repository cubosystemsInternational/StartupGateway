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

namespace StartupGateway.BusinessEntities
{
    [Table("chats")]
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public required string ChatTitle { get; set; }
        public required string ChatBody { get; set; }
        public ChatStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    // Used as a Temporary will need to change to a common share class
    public enum ChatStatus
    {
        Active,
        Inactive,
        Pending
    }
}
