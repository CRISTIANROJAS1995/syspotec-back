using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Entities;

namespace SyspotecDomain.IServices
{
    public interface IChallengeService
    {
        Task<List<ChallengeResponseDto>?> GetAll(PaginationDto pagination);
        Task<List<ChallengeDto>?> GetAllSummary();
    }
}
