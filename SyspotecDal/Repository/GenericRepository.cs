using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDal.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

            
        public async Task<AppConfiguration?> GetAppConfiguration()
        {
            var response = await _context.AppConfiguration.FirstOrDefaultAsync();
            return response;
        }

        public async Task<List<Gender>?> GetAllGender()
        {
            var response = await _context.Gender.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<InstrumentInterest>?> GetAllInstrumentInterest()
        {
            var response = await _context.InstrumentInterest.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<MusicalInterest>?> GetAllMusicalInterest()
        {
            var response = await _context.MusicalInterest.OrderBy(c => c.Order).ToListAsync();
            return response;
        }

        public async Task<List<SyspotecDomain.Entities.Range>?> GetAllRange()
        {
            var response = await _context.Range.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<SocialInterest>?> GetAllSocialInterest()
        {
            var response = await _context.SocialInterest.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<State>?> GetAllState()
        {
            var response = await _context.State.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<Store>?> GetAllStore()
        {
            var response = await _context.Store.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<SubscriptionDto>?> GetAllSubscription()
        {
            List<SubscriptionDto> lstSubscription = new List<SubscriptionDto>();
            var response = await _context.Subscription.Include("TypeSubscription").OrderBy(c => c.Id).ToListAsync();
            if (response.Count > 0) {
                foreach (var item in response)
                {
                    TypeSubscriptionDto objType = new TypeSubscriptionDto();
                    objType.Id = item.TypeSubscription.Id;
                    objType.Name = item.TypeSubscription.Name;

                    SubscriptionDto obj = new SubscriptionDto();
                    obj.Id = item.Id;
                    //obj.TypeSubscriptionId = item.TypeSubscriptionId;
                    obj.TypeSubscription = objType;
                    obj.Name = item.Name;
                    obj.Price = item.Price;
                    obj.CreatedDate = item.CreatedDate;
                    obj.UpdateDate = item.UpdateDate;

                    lstSubscription.Add(obj);
                }
            }
            return lstSubscription;
        }

        public async Task<List<TypeSubscription>?> GetAllTypeSubscription()
        {
            var response = await _context.TypeSubscription.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<TypeImage>?> GetAllTypeImage()
        {
            var response = await _context.TypeImage.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<TypeReaction>?> GetAllTypeReaction()
        {
            var response = await _context.TypeReaction.OrderBy(c => c.Id).ToListAsync();
            return response;
        }

        public async Task<List<DailyAchievement>?> GetAllDailyAchievement()
        {
            return await _context.DailyAchievement.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<List<PlayListGenericResponseDto>?> GetAllPlayList()
        {
            List<PlayListGenericResponseDto> response = new List<PlayListGenericResponseDto>();
            var consult = await _context.PlayList
                                .Include("TypePlayList")
                                .Include("State")
                                .OrderBy(c => c.Id)
                                .ToListAsync();

            if (consult.Count > 0) {
                response.AddRange(consult.AsEnumerable().Select(g => GetPlayListResponseDto(g)).ToList()!);
            }

            return response;
        }

        private PlayListGenericResponseDto GetPlayListResponseDto(PlayList consult)
        {
            PlayListGenericResponseDto response = new PlayListGenericResponseDto();

            if (consult != null)
            {
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                //TypePlayListDto objType = new TypePlayListDto();
                //objType.Id = consult.TypePlayList.Id;
                //objType.Name = consult.TypePlayList.Name;

                response.State = objState;
              //  response.TypePlayList = objType;
                response.CoverImage = consult.CoverImage;
                response.Title = consult.Title;
                response.Description = consult.Description;
            }

            return response;
        }
    }
}
