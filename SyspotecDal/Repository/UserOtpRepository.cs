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
    public class UserOtpRepository : IUserOtpRepository
    {
        private readonly ApplicationDbContext _context;

        public UserOtpRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(UserOtp model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserOtp model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserOtp>?> GetAllByEmail(string email)
        {
            return await _context.UserOtp.Where(r => r.Email == email && r.IsValid == true).ToListAsync();
        }

        public async Task<UserOtp?> ValidOtp(ValidOtpDto model)
        {
            var obj = await _context.UserOtp.FirstOrDefaultAsync(c => c.Email == model.Email && c.Code == model.Code.ToString() && c.IsValid == true);
            return obj;
        }   

    }
}
