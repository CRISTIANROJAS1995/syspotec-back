using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDal.Repository
{
    public class UserActivationRepository : IUserActivationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserActivationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(UserActivation model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserActivation model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserActivation?> GetByEmail(string email)
        {
            var obj = await _context.UserActivation.FirstOrDefaultAsync(c => c.Email == email);
            return obj;
        }

        public async Task<UserActivation?> GetByEmailActivation(string email)
        {
            var obj = await _context.UserActivation.FirstOrDefaultAsync(c => c.Email == email && c.EmailConfirm == true);
            return obj;
        }

    }
}
