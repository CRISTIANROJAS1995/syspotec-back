using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = SyspotecDomain.Entities.Range;

namespace SyspotecApplication.Services
{
    public class GenericService : IGenericService
    {
        private readonly IGenericRepository _genericRepository;

        public GenericService(
            IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<AppConfiguration?> GetAppConfiguration()
        {
            var response = await _genericRepository.GetAppConfiguration();
            return response;
        }

        public async Task<List<Gender>?> GetAllGender()
        {
            var response = await _genericRepository.GetAllGender();
            return response;
        }

        public async Task<List<InstrumentInterest>?> GetAllInstrumentInterest()
        {
            var response = await _genericRepository.GetAllInstrumentInterest();
            return response;
        }

        public async Task<List<MusicalInterest>?> GetAllMusicalInterest()
        {
            var response = await _genericRepository.GetAllMusicalInterest();
            return response;
        }

        public async Task<List<Range>?> GetAllRange()
        {
            var response = await _genericRepository.GetAllRange();
            return response;
        }

        public async Task<List<SocialInterest>?> GetAllSocialInterest()
        {
            var response = await _genericRepository.GetAllSocialInterest();
            return response;
        }

        public async Task<List<State>?> GetAllState()
        {
            var response = await _genericRepository.GetAllState();
            return response;
        }

        public async Task<List<Store>?> GetAllStore()
        {
            var response = await _genericRepository.GetAllStore();
            return response;
        }

        public async Task<List<SubscriptionDto>?> GetAllSubscription()
        {
            var response = await _genericRepository.GetAllSubscription();
            return response;
        }

        public async Task<List<TypeSubscription>?> GetAllTypeSubscription()
        {
            var response = await _genericRepository.GetAllTypeSubscription();
            return response;
        }

        public async Task<List<TypeImage>?> GetAllTypeImage()
        {
            var response = await _genericRepository.GetAllTypeImage();
            return response;
        }

        public async Task<List<TypeReaction>?> GetAllTypeReaction()
        {
            var response = await _genericRepository.GetAllTypeReaction();
            return response;
        }

        public async Task<List<DailyAchievement>?> GetAllDailyAchievement()
        {
            return await _genericRepository.GetAllDailyAchievement();
        }

        public async Task<List<PlayListGenericResponseDto>?> GetAllPlayList()
        {
            return await _genericRepository.GetAllPlayList();
        }
    }
}
