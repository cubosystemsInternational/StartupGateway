/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model UserDetails Created.
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
    /// <inheritdoc />
    ///<summary>
    /// Temporary table name, will confirm and update later.
    /// </summary>
    /// <returns></returns>
    [Table("UserDetails")]
    public class UserDetails
    {
        [Key]
        public int UserDetailsId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("User")]
        public int UserId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Not Confirmed whether value is nullable.
        /// </summary>
        ///<returns></returns>
        public required string FirstName { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Not Confirmed whether value is nullable.
        /// </summary>
        ///<returns></returns>
        public required string LastName { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set;}
    }
}
