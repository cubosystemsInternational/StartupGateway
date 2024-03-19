/**
 * Created by: Zaid
 * Modified by : Ibrahim
 * Created on: 18/03/2024
 * Description: Document class Model
 * 
 * */

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("documents")]

    public class Document
    {
        public int DocumentId { get; set; }
        public string? DocumentTitle { get; set; }
        public string? DocumentBody { get; set; }
        public string? DocumentContent { get; set; }
        public string? DocumentType { get; set; }
        public DocumentStatus Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
    // Used as a Temporary will need to change to a common share class
    public enum DocumentStatus
    {
        Active,
        Inactive,
        Pending
    }
}

