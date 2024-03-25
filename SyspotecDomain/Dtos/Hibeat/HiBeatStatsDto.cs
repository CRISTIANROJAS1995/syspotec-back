using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class HiBeatStatsDto
    {
        public int TotalMonthlyListener { get; set; }
        public int TotalReproduction { get; set; }
        public int TotalLike { get; set; }
        public int TotalShare { get; set; }
        public int TotalComment { get; set; }
        public List<HiBeatStatsCountryDto> LstContry { get; set; }
        public List<HiBeatStatsAgeDto> LstAge { get; set; }
        public List<HiBeatStatsGenderDto> LstGender { get; set; }
    }
}
