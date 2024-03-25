using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SyspotecDal.Repository
{
    public class UserFollowerRepository : IUserFollowerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserFollowerRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Add(UserFollower model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(UserFollower model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserFollower?> GetById(int id)
        {
            var obj = await _context.UserFollower
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            return obj;
        }

        public async Task<List<UserFollower>?> GetAllByIsRead()
        {
            return await _context.UserFollower
                .AsNoTracking()
                .Where(c => c.IsRead == false)
                .ToListAsync();
        }

        public async Task<int?> Delete(UserFollower model)
        {
            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserFollower>?> GetAllByUser(int userId)
        {
            return await _context.UserFollower.AsNoTracking().Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<UserFollower?> GetUserFollow(int userId, int userIdFollow)
        {
            return await _context.UserFollower.AsNoTracking().Where(r => r.UserId == userId && r.UserIdFollower == userIdFollow).FirstOrDefaultAsync();
        }

        public async Task<List<UserFollower>?> GetAllByUserPagination(int userId, PaginationDto pagination)
        {
            List<UserFollower> lstResponse = new List<UserFollower>();
            var queryable = _context.UserFollower
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .AsQueryable();

            await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

            var list = await queryable
                .OrderBy(c => c.CreatedDate)
                .Paginate(pagination)
                .ToListAsync();

            if (list.Count > 0)
            {
                lstResponse = list;
            }

            return lstResponse;
        }

        public List<ReactionResponseDto> GetAllUserFollowNotification(int userIdFollow)
        {
            List<ReactionResponseDto> lstResponse = new List<ReactionResponseDto>();

            var list = _context.UserFollower
                .AsNoTracking()
                .Include("User")
                .Include("User.Subscription")
                .Include("User.Gender")
                .Include("User.State")
                .Include("User.UserImage")
                .Include("User.UserImage.State")
                .Include("User.UserImage.TypeImage")
                .Where(r => r.UserIdFollower == userIdFollow
                    && r.CreatedDate.Year.ToString() + r.CreatedDate.Month.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString())
                .ToList();

            if (list.Count > 0)
            {
                lstResponse.AddRange(list.AsEnumerable().Select(g => GetNotificationResponse(g)).ToList()!);
            }

            return lstResponse;
        }

        private ReactionResponseDto GetNotificationResponse(UserFollower? consult)
        {
            ReactionResponseDto response = new ReactionResponseDto();

            if (consult != null)
            {
                TypeReactionDto objTypeReaction = new TypeReactionDto();
                objTypeReaction.Id = (int)TypeReactionEnum.Follow;
                objTypeReaction.Name = "follow";
                objTypeReaction.Point = 0;

                StateDto objState = new StateDto();
                objState.Id = (int)StateEnum.Active;
                objState.Name = "Active";

                response.User = GetUserResponseSummary(consult.User);

                response.Id = consult.Id;
                response.TypeReaction = objTypeReaction;
                response.State = objState;
                response.Description = string.Empty;
                response.IsRead = consult.IsRead;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
            }

            return response;
        }

        private UserResponseSummaryDto GetUserResponseSummary(User? consult)
        {
            UserResponseSummaryDto response = new UserResponseSummaryDto();

            if (consult != null)
            {
                var consultTypeSubscription = _context.TypeSubscription.AsNoTracking().FirstOrDefault(u => u.Id == consult.Subscription.TypeSubscriptionId);
                TypeSubscriptionDto objTypeSubscription = new TypeSubscriptionDto();

                if (consultTypeSubscription != null)
                {
                    objTypeSubscription.Id = consultTypeSubscription.Id;
                    objTypeSubscription.Name = consultTypeSubscription.Name;
                }

                SubscriptionDto objSubscription = new SubscriptionDto();
                objSubscription.Id = consult.Subscription.Id;

                objSubscription.TypeSubscription = objTypeSubscription;
                objSubscription.Name = consult.Subscription.Name;
                objSubscription.Price = consult.Subscription.Price;
                objSubscription.CreatedDate = consult.Subscription.CreatedDate;
                objSubscription.UpdateDate = consult.Subscription.UpdateDate;

                GenderDto objGender = new GenderDto();
                objGender.Id = consult.Gender.Id;
                objGender.Name = consult.Gender.Name;

                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.Subscription = objSubscription;
                response.Gender = objGender;
                response.State = objState;
                response.Name = consult.Name;
                response.UserName = consult.UserName.Trim();
                response.ArtistName = consult.ArtistName.Trim();
                response.Email = consult.Email;
                response.Nationality = consult.Nationality.Trim();
                response.CodeHfa = consult.CodeHfa;
                response.CodeNationality = consult.CodeNationality;
                response.BirthDate = consult.BirthDate;
                response.IsVerified = consult.IsVerified;
                response.DeviceToken = consult.DeviceToken;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
                response.CoinAmount = consult.CoinAmount;
                response.PointAmount = consult.PointAmount;
                response.IsMigration = consult.IsMigration;

                response.ListImage = GetAllImageUser(consult.UserImage.ToList());
            }

            return response;
        }

        private List<UserImageDto> GetAllImageUser(List<UserImage> consult)
        {
            List<UserImageDto> response = new List<UserImageDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (UserImage item in consult)
                    {
                        UserImageDto obj = new UserImageDto();

                        StateDto objStateImage = new StateDto();
                        objStateImage.Id = item.State.Id;
                        objStateImage.Name = item.State.Name;

                        TypeImageDto objTypeImage = new TypeImageDto();
                        objTypeImage.Id = item.TypeImage.Id;
                        objTypeImage.Name = item.TypeImage.Name;

                        obj.State = objStateImage;
                        obj.TypeImage = objTypeImage;
                        obj.Url = item.Url;
                        obj.CreatedDate = item.CreatedDate;
                        obj.UpdateDate = item.UpdateDate;

                        response.Add(obj);
                    }
                }
            }

            return response;
        }

    }
}
