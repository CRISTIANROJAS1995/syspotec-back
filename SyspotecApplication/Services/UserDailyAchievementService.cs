using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Generic;
using System.Linq.Expressions;

namespace SyspotecApplication.Services
{
    public class UserDailyAchievementService : IUserDailyAchievementService
    {
        private readonly IUserDailyAchievementRepository _userDailyAchievementRepository;

        public UserDailyAchievementService(
            IUserDailyAchievementRepository userDailyAchievementRepository)
        {
            _userDailyAchievementRepository = userDailyAchievementRepository;
        }

        public async Task<List<UserDailyAchievementDto>> GetDailyAchievement(string userId)
        {
            return await _userDailyAchievementRepository.GetDailyAchievement(userId);
        }

    }
}
