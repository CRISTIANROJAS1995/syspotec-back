using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace SyspotecDal.Repository
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReactionRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Add(Reaction model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(Reaction model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<Reaction?> GetById(int id)
        {
            var obj = await _context.Reaction
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return obj;
        }

        public async Task<Reaction?> GetIsValid(int userId, int typeReactionId, int hibeatId)
        {
            var obj = await _context.Reaction
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    c => c.UserId == userId &&
                    c.TypeReactionId == typeReactionId &&
                    c.HiBeatId == hibeatId &&
                    c.StateId == (int)StateEnum.Active
                );
            return obj;
        }

        public async Task<List<Reaction>?> GetByUserDailyAchievement(int userId, int typeReactionId)
        {
            return await _context.Reaction
                .AsNoTracking()
                .Where(
                    c => c.UserId == userId &&
                    c.TypeReactionId == typeReactionId &&
                    c.StateId == (int)StateEnum.Active &&
                    (c.UpdateDate.Year == DateTime.Now.Year && c.UpdateDate.Month == DateTime.Now.Month && c.UpdateDate.Day == DateTime.Now.Day)
                ).ToListAsync();
        }

    }
}
