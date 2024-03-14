using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessEntities.ReqModels
{
    public class CreateProjectModel
    {
        public string? ProjectName { get; set; }
        
        public string? ProjectTitle { get; set; }

        public string? ProjectDescription { get; set;}
    }
}
