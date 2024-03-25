using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserDailyAchievementDto
    {
        public DailyAchievementDto DailyAchievement { get; set; }

        public double Progress { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
