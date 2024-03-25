using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IHiBeatInstrumentInterestRepository
    {
        Task<int?> Add(HiBeatInstrumentInterest model);
        Task<int?> Delete(IList<HiBeatInstrumentInterest> model);
        Task<List<HiBeatInstrumentInterest>?> GetAllByHibeatId(int hibeatId);
        Task<HiBeatInstrumentInterest?> GetIsValid(int hibeatId, int instrumentInterestId);
    }
}
