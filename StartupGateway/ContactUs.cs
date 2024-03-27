﻿/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Model ChatDetails Created.
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
    [Table("contact_us")]
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public required string Email {  get; set; }
        public required string Subject { get; set; }
        public string? Message { get; set; }
        public EntityStatus Status {  get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}