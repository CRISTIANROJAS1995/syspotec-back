using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Entities;

namespace SyspotecDomain.IRepositories
{
    public interface IChallengeRepository
    {
        Task<List<ChallengeResponseDto>?> GetAll(PaginationDto pagination);
        Task<List<HibeatResponseDto>> GetChallengeHibeatAsync(Challenge challenge, PaginationDto pagination);
        Task<List<ChallengeDto>?> GetAllSummary();
    }
}
