/**
 * Created by: Ibrahim
 * Created on: 18/03/2024
 * Description: User class Model
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
    [Table("users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public  int UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }

    // Used as a Temporary will need to change to a common share class
    public enum UserStatus
    {
        Active,
        Inactive,
        Pending
    }
}
