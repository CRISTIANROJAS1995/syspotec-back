using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserDailyAchievementRepository
    {
        Task<int?> Add(UserDailyAchievement model);
        Task<int?> Update(UserDailyAchievement model);
        Task<DailyAchievement?> GetRandomDaily();
        Task<UserDailyAchievement?> GetDailyByUser(int userId);
        Task<List<UserDailyAchievementDto>> GetDailyAchievement(string userId);
        Task<List<UserFollower>?> GetAllFollowerDailyByUser(int userId);
    }
}
