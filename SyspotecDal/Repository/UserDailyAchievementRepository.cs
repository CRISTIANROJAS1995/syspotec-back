using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Generic;

namespace SyspotecDal.Repository
{
    public class UserDailyAchievementRepository : IUserDailyAchievementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository _genericRepository;
        private readonly IReactionRepository _reactionRepository;

        public UserDailyAchievementRepository(
            ApplicationDbContext context,
            IUserRepository userRepository,
            IGenericRepository genericRepository,
            IReactionRepository reactionRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _genericRepository = genericRepository;
            _reactionRepository = reactionRepository;
        }

        public async Task<int?> Add(UserDailyAchievement model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserDailyAchievement model)
        {
            _context.ChangeTracker.Clear();
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<DailyAchievement?> GetRandomDaily()
        {
            return await _context.DailyAchievement
                .AsNoTracking()
                .OrderBy(c => Guid.NewGuid())
                .FirstOrDefaultAsync();
        }

        public async Task<UserDailyAchievement?> GetDailyByUser(int userId)
        {
            return await _context.UserDailyAchievement
                .AsNoTracking()
                .Include("DailyAchievement")
                .Where(r => r.UserId == userId && r.CreatedDate.Year.ToString() + r.CreatedDate.Month.ToString() + r.CreatedDate.Day.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString())
                .FirstOrDefaultAsync();
        }

        public async Task<List<UserDailyAchievementDto>> GetDailyAchievement(string userId)
        {
            List<UserDailyAchievementDto> response = new List<UserDailyAchievementDto>();
            var consultUser = await _userRepository.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultDaily = await GetDailyByUser(consultUser.Id);
                if (consultDaily == null)
                {
                    var saveData = await SaveData(consultUser);
                    if (saveData!.Result)
                    {
                        var userDaily = await GetDailyByUser(consultUser.Id);
                        if (userDaily != null)
                        {
                            response.Add(await ValidData(userDaily, consultUser));
                        }
                    }
                }
                else
                {
                    response.Add(await ValidData(consultDaily, consultUser));
                }
            }

            return response;
        }

        public async Task<List<UserFollower>?> GetAllFollowerDailyByUser(int userId)
        {
            return await _context.UserFollower
                .AsNoTracking()
                .Where(r => r.UserId == userId && r.CreatedDate.Year.ToString() + r.CreatedDate.Month.ToString() + r.CreatedDate.Day.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString())
                .ToListAsync();
        }

        private async Task<ResponseApiDto?> SaveData(User user)
        {
            var response = new ResponseApiDto();

            var daily = await GetRandomDaily();
            if (daily != null)
            {
                UserDailyAchievement model = new UserDailyAchievement();

                model.UserId = user.Id;
                model.DailyAchievementId = daily.Id;
                model.Progress = 0;
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                var responseAdd = await Add(model);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar los retos diarios del usuario.";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado no existen retos diarios.";
            }


            return response;
        }

        private async Task<UserDailyAchievementDto> ValidData(UserDailyAchievement item, User user)
        {
            UserDailyAchievementDto response = new UserDailyAchievementDto();
            List<Reaction>? consultReaction = new List<Reaction>();
            List<UserFollower>? consultFollow = new List<UserFollower>();

            switch (item.DailyAchievementId)
            {
                case (int)DailyAchievementEnum.Follow:
                    consultFollow = await GetAllFollowerDailyByUser(user.Id);
                    break;
                case (int)DailyAchievementEnum.Like:
                    consultReaction = await _reactionRepository.GetByUserDailyAchievement(user.Id, (int)TypeReactionEnum.Like);
                    break;
                case (int)DailyAchievementEnum.Listen:
                    consultReaction = await _reactionRepository.GetByUserDailyAchievement(user.Id, (int)TypeReactionEnum.Reproduction);
                    break;
                case (int)DailyAchievementEnum.Announcement:               
                    break;
            }

            if (consultReaction!.Count > 0 || consultFollow!.Count > 0)
            {
                if (consultFollow!.Count > 0)
                {
                    item.Progress = consultFollow.Count >= item.DailyAchievement.Amount ? 100 : (((double)consultFollow.Count / (double)item.DailyAchievement.Amount) * 100);
                }
                else {
                    item.Progress = consultReaction.Count >= item.DailyAchievement.Amount ? 100 : (((double)consultReaction.Count / (double)item.DailyAchievement.Amount) * 100);
                }
               
                item.UpdateDate = DateTime.Now;

                if (!item.IsCompleted)
                {
                    if (consultReaction.Count >= item.DailyAchievement.Amount || consultFollow.Count >= item.DailyAchievement.Amount)
                    {
                        item.IsCompleted = true;
                    }

                    await Update(item);

                    if (item.IsCompleted)
                    {
                        user.UpdateDate = DateTime.Now;
                        user.CoinAmount += item.DailyAchievement.Coin;

                        await _userRepository.UpdateCoin(user);
                    }
                }
            }

            DailyAchievementDto objDailyAchievement = new DailyAchievementDto();
            objDailyAchievement.Name = item.DailyAchievement.Name;
            objDailyAchievement.Description = item.DailyAchievement.Description;
            objDailyAchievement.Amount = item.DailyAchievement.Amount;
            objDailyAchievement.Coin = item.DailyAchievement.Coin;
            objDailyAchievement.CreatedDate = item.DailyAchievement.CreatedDate;
            objDailyAchievement.UpdateDate = item.DailyAchievement.UpdateDate;

            response.DailyAchievement = objDailyAchievement;
            response.Progress = item.Progress;
            response.CreatedDate = item.CreatedDate;
            response.UpdateDate = item.UpdateDate;
            response.IsCompleted = item.IsCompleted;

            return response;
        }

    }
}
