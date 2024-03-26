using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Configuration
    {
        public int Id { get; set; }

        [Required]
        public string AcceptPersonalData { get; set; }

        [Required]
        public string AcceptMediaUse { get; set; }

        [Required]
        public string TemplateContract { get; set; }
    }
}
