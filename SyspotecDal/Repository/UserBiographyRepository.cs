using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal.Repository
{
    public class UserBiographyRepository : IUserBiographyRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBiographyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(UserBiography model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserBiography model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserBiography?> GetByUserId(int userId)
        {
            var consult = await _context.UserBiography
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(c => c.CreatedDate)
                .FirstOrDefaultAsync();

            return consult;
        }
    }
}
