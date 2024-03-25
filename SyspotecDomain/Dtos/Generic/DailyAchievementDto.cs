using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Generic
{
    public class DailyAchievementDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public int Coin { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
