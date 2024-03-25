using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Generic;

namespace SyspotecDomain.IServices
{
    public interface IHiBeatInstrumentInterestService
    {
        Task<ResponseApiDto?> Add(int hibeatId, InstrumentInterestDto request);
        Task<ResponseApiDto?> Delete(int hibeatId);
        Task<List<HiBeatInstrumentInterest>?> GetAllByHibeatId(int hibeatId);
        Task<HiBeatInstrumentInterest?> GetIsValid(int hibeatId, int instrumentInterestId);
    }
}



