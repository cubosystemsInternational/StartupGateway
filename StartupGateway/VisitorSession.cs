/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model VisitorSession Created.
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
    [Table("VisitorSession")]
    public class VisitorSession
    {
        [Key]
        public int VisitorSessionId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        /// <inheritdoc/>
        /// <summary>
        /// Duration should idealy be a float value, Should discuss this.
        /// </summary>
        /// <remove></remove>
        public int Duration { get; set; }
        /// <inheritdoc/>
        /// <summary>
        /// The Documentation considers this value to be an int. Should be a string.
        /// Not Confirmed whether value is nullable.
        /// </summary>
        /// <remove></remove>
        public string? IpAddress { get; set; }
        public AvailabilityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
