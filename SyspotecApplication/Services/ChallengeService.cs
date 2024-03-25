using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Entities;

namespace SyspotecApplication.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<List<ChallengeResponseDto>?> GetAll(PaginationDto pagination)
        {
            var list = await _challengeRepository.GetAll(pagination);
            return list;
        }

        public async Task<List<ChallengeDto>?> GetAllSummary()
        {
            var list = await _challengeRepository.GetAllSummary();
            return list;
        }
    }
}
