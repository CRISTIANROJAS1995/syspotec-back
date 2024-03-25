using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal.Repository
{
    public class UserBlockRepository : IUserBlockRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBlockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(UserBlock model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(UserBlock model)
        {
            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserBlock>?> GetAllByUser(int userId)
        {
            return await _context.UserBlock.AsNoTracking().Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<UserBlock?> GetUserBlock(int userIdBlock)
        {
            return await _context.UserBlock.AsNoTracking().Where(r => r.UserIdBlock == userIdBlock).FirstOrDefaultAsync();
        }

    }
}
