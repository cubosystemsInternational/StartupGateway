using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartupGateway.Model
{
    [Table("documents")]

    public class Documents
	{
        public int DocumentId { get; set; }
        public string? DocumentTitle { get; set; }
        public string? DocumentSize { get; set; }
        public int Status { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}

