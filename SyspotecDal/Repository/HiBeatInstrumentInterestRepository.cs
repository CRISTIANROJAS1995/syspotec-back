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
    public class HiBeatInstrumentInterestRepository : IHiBeatInstrumentInterestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public HiBeatInstrumentInterestRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<int?> Add(HiBeatInstrumentInterest model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(IList<HiBeatInstrumentInterest> model)
        {
            _context.RemoveRange(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<HiBeatInstrumentInterest>?> GetAllByHibeatId(int hibeatId)
        {
            return await _context.HiBeatInstrumentInterest.AsNoTracking().Where(r => r.HiBeatId == hibeatId).ToListAsync();
        }

        public async Task<HiBeatInstrumentInterest?> GetIsValid(int hibeatId, int instrumentInterestId)
        {
            return await _context.HiBeatInstrumentInterest
                .AsNoTracking()
                .Where(
                    r => r.HiBeatId == hibeatId &&
                    r.InstrumentInterestId == instrumentInterestId
                ).FirstOrDefaultAsync();
        }
    }
}
