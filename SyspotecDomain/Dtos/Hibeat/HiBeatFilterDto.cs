using SyspotecDomain.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class HiBeatFilterDto
    {
        public string? All { get; set; }
        public string? Hibeat { get; set; }

        public string? Artist { get; set; }

        public MusicalInterestDto? MusicalInterest { get; set; }

    }
}
