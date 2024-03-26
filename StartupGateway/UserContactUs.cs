/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Model UserContactUs Created.
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
    [Table("user_contact_us")]
    public class UserContactUs
    {
        [Key]
        public int Id {  get; set; }
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        [ForeignKey("users")]
        public int UserId { get; set; }
        /// <summary>
        /// Foreign key table name is temporary.
        /// </summary>
        [ForeignKey("contact_us")]
        public int ContactUsId { get; set; }
        public EntityStatus Status { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set;}
    }
}
