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
    ///<summary>
    ///Refer to documentation regarding model structure, naming conventions and table names.
    ///<see href="https://docs.google.com/spreadsheets/d/10V5CjHCM5KkOb9o3CiNe-QjgNh8gp8ny8zf802KcUoE/edit?usp=sharing"> Documentation.</see>
    ///</summary>
    [Table("documents")]

    public class Documents
    {
        public int Id { get; set; }
        public required string DocumentTitle { get; set; }
        public required string? DocumentBody { get; set; }
        public required string DocumentContent { get; set; }
        public required string DocumentType { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
  
}

