using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IHiBeatMusicalInterestRepository
    {
        Task<int?> Add(HiBeatMusicalInterest model);
        Task<int?> Delete(IList<HiBeatMusicalInterest> model);
        Task<List<HiBeatMusicalInterest>?> GetAllByHibeatId(int hibeatId);
        Task<HiBeatMusicalInterest?> GetIsValid(int hibeatId, int musicalInterestId);
    }
}
