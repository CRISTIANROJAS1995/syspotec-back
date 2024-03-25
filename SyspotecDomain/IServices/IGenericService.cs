using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Range = SyspotecDomain.Entities.Range;

namespace SyspotecDomain.IServices
{
    public interface IGenericService
    {
        Task<AppConfiguration?> GetAppConfiguration();
        Task<List<Gender>?> GetAllGender();
        Task<List<InstrumentInterest>?> GetAllInstrumentInterest();
        Task<List<MusicalInterest>?> GetAllMusicalInterest();
        Task<List<Range>?> GetAllRange();
        Task<List<SocialInterest>?> GetAllSocialInterest();
        Task<List<State>?> GetAllState();
        Task<List<Store>?> GetAllStore();
        Task<List<SubscriptionDto>?> GetAllSubscription();
        Task<List<TypeSubscription>?> GetAllTypeSubscription();
        Task<List<TypeImage>?> GetAllTypeImage();
        Task<List<TypeReaction>?> GetAllTypeReaction();
        Task<List<DailyAchievement>?> GetAllDailyAchievement();
        Task<List<PlayListGenericResponseDto>?> GetAllPlayList();
    }
}
