using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Generic
{
    public class CompanyDto
    {
        public string Identifier { get; set; }

        public StateDto State { get; set; }

        public string Name { get; set; }

        public string Nit { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
