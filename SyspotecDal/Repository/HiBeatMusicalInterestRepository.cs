using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SyspotecDal.Repository
{
    public class HiBeatMusicalInterestRepository : IHiBeatMusicalInterestRepository
    {
        private readonly ApplicationDbContext _context;

        public HiBeatMusicalInterestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Add(HiBeatMusicalInterest model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(IList<HiBeatMusicalInterest> model)
        {
            _context.RemoveRange(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<HiBeatMusicalInterest>?> GetAllByHibeatId(int hibeatId)
        {
            return await _context.HiBeatMusicalInterest.AsNoTracking().Where(r => r.HiBeatId == hibeatId).ToListAsync();
        }

        public async Task<HiBeatMusicalInterest?> GetIsValid(int hibeatId, int musicalInterestId)
        {
            return await _context.HiBeatMusicalInterest
                .AsNoTracking()
                .Where(
                    r => r.HiBeatId == hibeatId &&
                    r.MusicalInterestId == musicalInterestId
                ).FirstOrDefaultAsync();
        }
    }
}
