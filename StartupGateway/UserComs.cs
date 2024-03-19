/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model UserComs Created.
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
    /// <inheritdoc />
    ///<summary>
    /// Temporary table name, will confirm and update later.
    /// </summary>
    /// <returns></returns>
    [Table("UserComs")]
    public class UserComs
    {
        [Key]
        public int UserComId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("Com")]
        public int ComId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("User")]
        public int UserId { get; set; }
        public AvailabilityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }

    /// <inheritdoc />
    ///<summary>
    /// This is a temporary enum class, will remove once problem has been consolidated.
    /// </summary>
    /// <returns></returns>
    public enum AvailabilityStatus 
    {
        Active,
        Inactive,
        Pending
    }
}
