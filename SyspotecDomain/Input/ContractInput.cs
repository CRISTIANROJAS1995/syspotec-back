using System;
using System.ComponentModel.DataAnnotations;

namespace SyspotecDomain.Input
{
    public class ContractInput
    {
        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
