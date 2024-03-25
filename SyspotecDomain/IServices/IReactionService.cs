using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Entities;

namespace SyspotecDomain.IServices
{
    public interface IReactionService
    {
        Task<ResponseApiDto?> Add(string userId, ReactionDto request);
        Task<ResponseApiDto?> Update(string userId, ReactionDto request);
        Task<Reaction?> GetById(int id);
        Task<ResponseApiDto?> UpdateNotification(int id);
        Task<Reaction?> GetIsValid(int userId, int typeReactionId, int hibeatId);
        Task<List<Reaction>?> GetByUserDailyAchievement(int userId, int typeReactionId);
    }
}
