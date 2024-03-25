using SyspotecDomain.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class HiBeatMusicalInterestDto
    {
        public MusicalInterestDto MusicalInterest { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
