/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model DeviceType Created.
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
    [Table("DeviceType")]
    public class DeviceType
    {
        [Key]
        public int DeviceId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Not Confirmed whether value is nullable.
        /// </summary>
        ///<returns></returns>
        public string DeviceName { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Not Confirmed whether value is nullable.
        /// </summary>
        ///<returns></returns>
        public string DeviceModel { get; set; }
        public AvailabilityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
