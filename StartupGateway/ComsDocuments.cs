/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model ComsDocuments Created.
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
    [Table("ComsDocuments")]
    public class ComsDocuments
    {
        [Key]
        public int ComsDocumentsId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("Document")]
        public int DocumentId { get; set; }
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
        public DateTime ModifiedOn { get; set; }
    }
}
