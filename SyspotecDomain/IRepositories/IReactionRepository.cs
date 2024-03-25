using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IReactionRepository
    {
        Task<int?> Add(Reaction model);
        Task<int?> Update(Reaction model);
        Task<Reaction?> GetById(int id);
        Task<Reaction?> GetIsValid(int userId, int typeReactionId, int hibeatId);
        Task<List<Reaction>?> GetByUserDailyAchievement(int userId, int typeReactionId);
    }
}
