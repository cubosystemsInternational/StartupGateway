/**
 * Created by: Shuaib
 * Created on: 18/03/2024
 * Description: Model CompanyDocuments Created.
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
    [Table("CompanyDocuments")]
    public class CompanyDocuments
    {
        [Key]
        public int CompanyDocumentId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("document")]
        public int DocumentId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        ///<returns></returns>
        [ForeignKey("company")]
        public int CompanyId { get; set; }
        ///<inheritdoc />
        /// <summary>
        /// Not Confirmed whether value is nullable.
        /// </summary>
        ///<returns></returns>
        public required string DocumentType { get; set; }
        public AvailabilityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
