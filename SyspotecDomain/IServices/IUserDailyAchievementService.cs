using SyspotecDomain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IUserDailyAchievementService
    {
        Task<List<UserDailyAchievementDto>> GetDailyAchievement(string userId);
    }
}
