using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal.Repository
{
    public class UserImageRepository : IUserImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserImageRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<int?> Add(UserImage model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(UserImage model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserImage?> GetIsValid(int userId, int typeImageId)
        {
            return await _context.UserImage.AsNoTracking().Where(
                r => r.UserId == userId && 
                r.StateId == (int)StateEnum.Active &&
                r.TypeImageId == typeImageId
            ).FirstOrDefaultAsync();
        }
    }
}
