using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal.Repository
{
    public class UserMapRepository : IUserMapRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserMapRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<int?> Add(UserMap model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserMap model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(UserMap model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserMap?> GetIsValid(int userId)
        {
            return await _context.UserMap.AsNoTracking().Where(
                r => r.UserId == userId &&
                r.StateId == (int)StateEnum.Active
            ).FirstOrDefaultAsync();
        }
    }
}
