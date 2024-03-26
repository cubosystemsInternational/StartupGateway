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
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessEntities
{
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("visitor_sessions")]
    public class VisitorSessions
    {
        [Key]
        public int Id { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("devices")]
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
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
