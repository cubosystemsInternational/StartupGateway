/**
 * Created by: Zaid
 * Modified by : Ibrahim
 * Created on: 18/03/2024
 * Description: Document class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using static StartupGateway.Shared.Share;

namespace StartupGateway.Model
{
    [Table("documents")]

    public class Document
    {
        public int DocumentId { get; set; }
        public required string DocumentTitle { get; set; }
        public required string? DocumentBody { get; set; }
        public required string DocumentContent { get; set; }
        public required string DocumentType { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
  
}

