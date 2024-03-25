using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
//using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using System.Linq;
using System.Web;

namespace SyspotecDal.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<List<UserResponseDto>?> GetAll(PaginationDto pagination)
        //{
        //    List<UserResponseDto> lstUserResponse = new List<UserResponseDto>();

        //    var queryable = _context.User
        //         .AsNoTracking()
        //         .Include("Subscription")
        //         .Include("Gender")
        //         .Include("State")
        //         .Include("UserBiography")
        //         .Include("UserBlock")
        //         .Include("UserImage")
        //         .Include("UserImage.State")
        //         .Include("UserImage.TypeImage")
        //         .Include("UserMap")
        //         .Include("UserMap.State")
        //         .Include("UserFollower")
        //         .AsQueryable();

        //    await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

        //    var list = await queryable
        //        .OrderBy(c => c.CreatedDate)
        //        .Paginate(pagination)
        //        .ToListAsync();

        //    if (list.Count > 0)
        //    {
        //        foreach (User user in list)
        //        {
        //            lstUserResponse.Add(await GetUserResponseAsync(user));
        //        }
        //    }

        //    return lstUserResponse;
        //}

        //public async Task<UserResponseDto> GetByIdentifier(string identifier)
        //{
        //    var obj = await _context.User
        //        .AsNoTracking()
        //        .Include("Subscription")
        //        .Include("Gender")
        //        .Include("State")
        //        .Include("UserBiography")
        //        .Include("UserBlock")
        //        .Include("UserImage")
        //        .Include("UserImage.State")
        //        .Include("UserImage.TypeImage")
        //        .Include("UserMap")
        //        .Include("UserMap.State")
        //        .Include("UserFollower")
        //        .FirstOrDefaultAsync(c => c.Identifier == identifier);

        //    return await GetUserResponseAsync(obj);
        //}

        //public UserResponseSummaryDto? GetByIdentifierSummary(string identifier, int pointSql = 0)
        //{
        //    var obj = _context.User
        //        .AsNoTracking()
        //        .Include("Subscription")
        //        .Include("Gender")
        //        .Include("State")
        //        .Include("UserBiography")
        //        .Include("UserBlock")
        //        .Include("UserImage")
        //        .Include("UserImage.State")
        //        .Include("UserImage.TypeImage")
        //        .Include("UserMap")
        //        .Include("UserMap.State")
        //        .Include("UserFollower")
        //        .FirstOrDefault(c => c.Identifier == identifier);

        //    return GetUserResponseSummary(obj, pointSql > 0 ? pointSql : 0);
        //}

        //public async Task<List<UserResponseDto>?> GetAllFilterByArtistName(PaginationDto pagination, string name)
        //{
        //    List<UserResponseDto> lstUserResponse = new List<UserResponseDto>();

        //    var queryable = _context.User
        //         .AsNoTracking()
        //         .Include("Subscription")
        //         .Include("Gender")
        //         .Include("State")
        //         .Include("UserBiography")
        //         .Include("UserBlock")
        //         .Include("UserImage")
        //         .Include("UserImage.State")
        //         .Include("UserImage.TypeImage")
        //         .Include("UserMap")
        //         .Include("UserMap.State")
        //         .Include("UserFollower")
        //         .Where(u => u.ArtistName.ToLower().Contains(name!))
        //         .AsQueryable();

        //    await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

        //    var list = await queryable
        //        .Paginate(pagination)
        //        .ToListAsync();

        //    if (list.Count > 0)
        //    {
        //        foreach (User user in list)
        //        {
        //            lstUserResponse.Add(await GetUserResponseAsync(user));
        //        }
        //    }

        //    return lstUserResponse;
        //}

        //public async Task<User?> GetByEmail(string email)
        //{
        //    var obj = await _context.User.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        //    return obj;
        //}

        //public async Task<User?> GetByUserName(string userName)
        //{
        //    var obj = await _context.User.AsNoTracking().FirstOrDefaultAsync(c => c.UserName == userName);
        //    return obj;
        //}

        //public async Task<User?> GetIdByIdentifier(string identifier)
        //{
        //    var obj = await _context.User.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier);
        //    return obj;
        //}

        //public async Task<UserResponseDto> GetValidCredentialAsync(RequestLoginDto model)
        //{
        //    var consult = _context.User
        //        .AsNoTracking()
        //        .Include("Subscription")
        //        .Include("Gender")
        //        .Include("State")
        //        .Include("UserBiography")
        //        .Include("UserBlock")
        //        .Include("UserImage")
        //        .Include("UserImage.State")
        //        .Include("UserImage.TypeImage")
        //        .Include("UserMap")
        //        .Include("UserMap.State")
        //        .Include("UserFollower")
        //        .FirstOrDefault((u => u.Email == model.Email && u.Password == model.Password));

        //    return await GetUserResponseAsync(consult);
        //}

        //public async Task<UserResponseDto> GetValidCredentialForgotAsync(RequestLoginDto model)
        //{
        //    var consult = _context.User
        //        .AsNoTracking()
        //          .FirstOrDefault((u => u.Email == model.Email));

        //    return await GetUserResponseForgotAsync(consult);
        //}

        //public async Task<int?> Add(User model)
        //{
        //    await _context.AddAsync(model);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<int?> Update(User model)
        //{
        //    _context.Update(model);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<ResponseApiDto?> UpdateCoin(User request)
        //{
        //    var response = new ResponseApiDto();
        //    User modelUpdate = new User();

        //    modelUpdate.Id = request.Id;
        //    modelUpdate.Identifier = request.Identifier;
        //    modelUpdate.SubscriptionId = request.SubscriptionId;
        //    modelUpdate.GenderId = request.GenderId;
        //    modelUpdate.StateId = request.StateId;
        //    modelUpdate.Name = request.Name;
        //    modelUpdate.UserName = request.UserName;
        //    modelUpdate.Password = request.Password;
        //    modelUpdate.ArtistName = request.ArtistName;
        //    modelUpdate.Email = request.Email;
        //    modelUpdate.Nationality = request.Nationality;
        //    modelUpdate.CodeHfa = request.CodeHfa;
        //    modelUpdate.CodeNationality = request.CodeNationality;
        //    modelUpdate.BirthDate = request.BirthDate;
        //    modelUpdate.IsVerified = request.IsVerified;
        //    modelUpdate.DeviceToken = request.DeviceToken;
        //    modelUpdate.CreatedDate = request.CreatedDate;
        //    modelUpdate.UpdateDate = DateTime.Now;
        //    modelUpdate.CoinAmount = request.CoinAmount;
        //    modelUpdate.PointAmount = request.PointAmount;

        //    var responseUpdate = await Update(modelUpdate);
        //    if (responseUpdate == 1)
        //    {
        //        response.Result = true;
        //    }
        //    else
        //    {
        //        response.Result = false;
        //        response.Message = "Ocurrio un error inesperado al actualizar las conins del usuario.";
        //    }

        //    return response;
        //}

        //public async Task<List<UserResponseSummaryDto>?> GetTopLastWeek()
        //{
        //    List<UserResponseSummaryDto> lstUserResponse = new List<UserResponseSummaryDto>();
        //    List<User> userSql = new List<User>();

        //    userSql = await _context.User.FromSqlRaw("TopArtistLastWeek").ToListAsync();
        //    if (userSql.Count > 0)
        //    {
        //        lstUserResponse.AddRange(userSql.AsEnumerable().Select(g => GetByIdentifierSummary(g.Identifier, g.PointAmount)).ToList()!);
        //    }

        //    return lstUserResponse;
        //}

        

        //#region Ranking Global

        //public async Task<List<UserResponseSummaryDto>?> GetRanking()
        //{
        //    List<UserResponseSummaryDto> lstUserResponse = new List<UserResponseSummaryDto>();
        //    List<User> userSql = new List<User>();

        //    userSql = await _context.User.FromSqlRaw("ConsultRanking").ToListAsync();
        //    if (userSql.Count > 0)
        //    {
        //        lstUserResponse.AddRange(userSql.AsEnumerable().Select(g => GetByIdentifierSummary(g.Identifier, g.PointAmount)).ToList()!);
        //    }

        //    return lstUserResponse;
        //}

        //#endregion


        //#region User Data DTO

        //private async Task<UserResponseSummaryDto> GetSummaryById(int id)
        //{
        //    var obj = await _context.User
        //        .AsNoTracking()
        //        .Include("Subscription")
        //        .Include("Gender")
        //        .Include("State")
        //        .Include("UserImage")
        //        .Include("UserImage.State")
        //        .Include("UserImage.TypeImage")
        //        .FirstOrDefaultAsync(c => c.Id == id);

        //    return GetUserResponseSummary(obj);
        //}

        //private async Task<UserResponseDto> GetUserResponseAsync(User? consult)
        //{
        //    UserResponseDto response = new UserResponseDto();

        //    if (consult != null)
        //    {
        //        //Subscription
        //        var consultTypeSubscription = _context.TypeSubscription.AsNoTracking().FirstOrDefault(u => u.Id == consult.Subscription.TypeSubscriptionId);
        //        TypeSubscriptionDto objTypeSubscription = new TypeSubscriptionDto();

        //        if (consultTypeSubscription != null)
        //        {
        //            objTypeSubscription.Id = consultTypeSubscription.Id;
        //            objTypeSubscription.Name = consultTypeSubscription.Name;
        //        }

        //        SubscriptionDto objSubscription = new SubscriptionDto();
        //        objSubscription.Id = consult.Subscription.Id;
        //        //objSubscription.TypeSubscriptionId = consult.Subscription.TypeSubscriptionId;
        //        objSubscription.TypeSubscription = objTypeSubscription;
        //        objSubscription.Name = consult.Subscription.Name;
        //        objSubscription.Price = consult.Subscription.Price;
        //        objSubscription.CreatedDate = consult.Subscription.CreatedDate;
        //        objSubscription.UpdateDate = consult.Subscription.UpdateDate;

        //        //Gender
        //        GenderDto objGender = new GenderDto();
        //        objGender.Id = consult.Gender.Id;
        //        objGender.Name = consult.Gender.Name;

        //        //State
        //        StateDto objState = new StateDto();
        //        objState.Id = consult.State.Id;
        //        objState.Name = consult.State.Name;

        //        response.Identifier = consult.Identifier;
        //        //response.SubscriptionId = consult.SubscriptionId;
        //        response.Subscription = objSubscription;
        //        //response.GenderId = consult.GenderId;
        //        response.Gender = objGender;
        //        //response.StateId = consult.StateId;
        //        response.State = objState;
        //        response.Name = consult.Name;
        //        response.UserName = consult.UserName.Trim();
        //        response.ArtistName = consult.ArtistName.Trim();
        //        response.Email = consult.Email;
        //        response.Nationality = consult.Nationality.Trim();
        //        response.CodeHfa = consult.CodeHfa;
        //        response.CodeNationality = consult.CodeNationality;
        //        response.BirthDate = consult.BirthDate;
        //        response.IsVerified = consult.IsVerified;
        //        response.DeviceToken = consult.DeviceToken;
        //        response.CreatedDate = consult.CreatedDate;
        //        response.UpdateDate = consult.UpdateDate;
        //        response.CoinAmount = consult.CoinAmount;
        //        response.PointAmount = consult.PointAmount;
        //        response.IsMigration = consult.IsMigration;

        //        response.Biography = GetBiography(consult.UserBiography.ToList());
        //        response.ListBlock = await GetAllBlock(consult);
        //        response.ListFollow = await GetAllFollow(consult);
        //        response.ListFollower = await GetAllFollower(consult.Id);
        //        response.ListImage = GetAllImage(consult.UserImage.ToList());
        //        response.Map = GetMap(consult.UserMap.ToList());

        //        //calc points of my hibeats
        //        //var consultHibeats = await _hibeatRepository.GetByUserIdentifier(consult.Identifier);

        //        //response.PointAmount = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Points) : 0;
        //        //response.LstHibeat = consultHibeats;

        //        ////stats
        //        //response.TotalMonthlyListener = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Stats!.TotalMonthlyListener) : 0;
        //        //response.TotalReproduction = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Stats!.TotalReproduction) : 0;
        //        //response.TotalLike = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Stats!.TotalLike) : 0;
        //        //response.TotalShare = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Stats!.TotalShare) : 0;
        //        //response.TotalComment = consultHibeats!.Count > 0 ? consultHibeats.Sum(r => r.Stats!.TotalComment) : 0;
        //    }

        //    return response;
        //}

        //private UserResponseSummaryDto GetUserResponseSummary(User? consult, int pointSql = 0)
        //{
        //    UserResponseSummaryDto response = new UserResponseSummaryDto();

        //    if (consult != null)
        //    {
        //        //Subscription
        //        var consultTypeSubscription = _context.TypeSubscription.AsNoTracking().FirstOrDefault(u => u.Id == consult.Subscription.TypeSubscriptionId);
        //        TypeSubscriptionDto objTypeSubscription = new TypeSubscriptionDto();

        //        if (consultTypeSubscription != null)
        //        {
        //            objTypeSubscription.Id = consultTypeSubscription.Id;
        //            objTypeSubscription.Name = consultTypeSubscription.Name;
        //        }

        //        SubscriptionDto objSubscription = new SubscriptionDto();
        //        objSubscription.Id = consult.Subscription.Id;
        //        //objSubscription.TypeSubscriptionId = consult.Subscription.TypeSubscriptionId;
        //        objSubscription.TypeSubscription = objTypeSubscription;
        //        objSubscription.Name = consult.Subscription.Name;
        //        objSubscription.Price = consult.Subscription.Price;
        //        objSubscription.CreatedDate = consult.Subscription.CreatedDate;
        //        objSubscription.UpdateDate = consult.Subscription.UpdateDate;

        //        //Gender
        //        GenderDto objGender = new GenderDto();
        //        objGender.Id = consult.Gender.Id;
        //        objGender.Name = consult.Gender.Name;

        //        //State
        //        StateDto objState = new StateDto();
        //        objState.Id = consult.State.Id;
        //        objState.Name = consult.State.Name;

        //        response.Identifier = consult.Identifier;
        //        //response.SubscriptionId = consult.SubscriptionId;
        //        response.Subscription = objSubscription;
        //        //response.GenderId = consult.GenderId;
        //        response.Gender = objGender;
        //        //response.StateId = consult.StateId;
        //        response.State = objState;
        //        response.Name = consult.Name;
        //        response.UserName = consult.UserName.Trim();
        //        response.ArtistName = consult.ArtistName.Trim();
        //        response.Email = consult.Email;
        //        response.Nationality = consult.Nationality.Trim();
        //        response.CodeHfa = consult.CodeHfa;
        //        response.CodeNationality = consult.CodeNationality;
        //        response.BirthDate = consult.BirthDate;
        //        response.IsVerified = consult.IsVerified;
        //        response.DeviceToken = consult.DeviceToken;
        //        response.CreatedDate = consult.CreatedDate;
        //        response.UpdateDate = consult.UpdateDate;
        //        response.CoinAmount = consult.CoinAmount;
        //        response.PointAmount = pointSql > 0 ? pointSql : consult.PointAmount;
        //        response.IsMigration = consult.IsMigration;

        //        response.ListImage = GetAllImage(consult.UserImage.ToList());
        //    }

        //    return response;
        //}

        //private async Task<List<UserBlockDto>> GetAllBlock(User? consult)
        //{
        //    List<UserBlockDto> response = new List<UserBlockDto>();

        //    if (consult != null)
        //    {
        //        if (consult.UserBlock.Count > 0)
        //        {
        //            foreach (UserBlock item in consult.UserBlock)
        //            {
        //                UserBlockDto obj = new UserBlockDto();

        //                obj.UserBlock = await GetSummaryById(item.UserIdBlock);
        //                obj.CreatedDate = item.CreatedDate;
        //                obj.UpdateDate = item.UpdateDate;

        //                response.Add(obj);
        //            }
        //        }
        //    }

        //    return response;
        //}

        //private List<UserImageDto> GetAllImage(List<UserImage> consult)
        //{
        //    List<UserImageDto> response = new List<UserImageDto>();

        //    if (consult != null)
        //    {
        //        if (consult.Count > 0)
        //        {
        //            foreach (UserImage item in consult)
        //            {
        //                UserImageDto obj = new UserImageDto();

        //                //State
        //                StateDto objStateImage = new StateDto();
        //                objStateImage.Id = item.State.Id;
        //                objStateImage.Name = item.State.Name;

        //                //Type Image
        //                TypeImageDto objTypeImage = new TypeImageDto();
        //                objTypeImage.Id = item.TypeImage.Id;
        //                objTypeImage.Name = item.TypeImage.Name;

        //                obj.State = objStateImage;
        //                obj.TypeImage = objTypeImage;
        //                obj.Url = item.Url;
        //                obj.CreatedDate = item.CreatedDate;
        //                obj.UpdateDate = item.UpdateDate;

        //                response.Add(obj);
        //            }
        //        }
        //    }

        //    return response;
        //}

        //private UserBiographyDto? GetBiography(List<UserBiography> consult)
        //{
        //    UserBiographyDto? response = new UserBiographyDto();
        //    List<UserBiographyDto> lst = new List<UserBiographyDto>();

        //    if (consult != null)
        //    {
        //        if (consult.Count > 0)
        //        {
        //            foreach (UserBiography item in consult)
        //            {
        //                UserBiographyDto obj = new UserBiographyDto();

        //                obj.Description = item.Description;
        //                obj.UrlFacebook = item.UrlFacebook;
        //                obj.UrlInstagram = item.UrlInstagram;
        //                obj.UrlSoundCloud = item.UrlSoundCloud;
        //                obj.UrlSpotify = item.UrlSpotify;
        //                obj.UrlWeb = item.UrlWeb;
        //                obj.UrlYoutube = item.UrlYoutube;
        //                obj.CreatedDate = item.CreatedDate;
        //                obj.UpdateDate = item.UpdateDate;

        //                lst.Add(obj);
        //            }
        //        }
        //    }

        //    if (lst.Count > 0)
        //    {
        //        return response = lst.OrderBy(c => c.CreatedDate).FirstOrDefault();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //private UserMapDto? GetMap(List<UserMap> consult)
        //{
        //    UserMapDto? response = new UserMapDto();
        //    List<UserMapDto> lst = new List<UserMapDto>();

        //    if (consult != null)
        //    {
        //        if (consult.Count > 0)
        //        {
        //            foreach (UserMap item in consult)
        //            {
        //                UserMapDto obj = new UserMapDto();

        //                //State
        //                StateDto objStateMap = new StateDto();
        //                objStateMap.Id = item.State.Id;
        //                objStateMap.Name = item.State.Name;

        //                obj.State = objStateMap;
        //                obj.Latitude = item.Latitude;
        //                obj.Longitude = item.Longitude;
        //                obj.CreatedDate = item.CreatedDate;
        //                obj.UpdateDate = item.UpdateDate;
        //                obj.Location = item.Location;

        //                lst.Add(obj);
        //            }
        //        }
        //    }

        //    if (lst.Count > 0)
        //    {
        //        return response = lst.OrderBy(c => c.CreatedDate).FirstOrDefault();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //private async Task<List<UserFollowDto>> GetAllFollow(User? consult)
        //{
        //    List<UserFollowDto> response = new List<UserFollowDto>();

        //    if (consult != null)
        //    {
        //        if (consult.UserFollower.Count > 0)
        //        {
        //            foreach (UserFollower item in consult.UserFollower)
        //            {
        //                UserFollowDto obj = new UserFollowDto();

        //                obj.UserFollow = await GetSummaryById(item.UserIdFollower);
        //                obj.CreatedDate = item.CreatedDate;
        //                obj.UpdateDate = item.UpdateDate;

        //                response.Add(obj);
        //            }
        //        }
        //    }

        //    return response;
        //}

        //private async Task<List<UserFollowerDto>> GetAllFollower(int userId)
        //{
        //    List<UserFollowerDto> response = new List<UserFollowerDto>();

        //    var listFollower = await _context.UserFollower
        //       .AsNoTracking()
        //       .Where(c => c.UserIdFollower == userId)
        //       .OrderBy(c => c.CreatedDate)
        //       .ToListAsync();

        //    if (listFollower.Count > 0)
        //    {
        //        foreach (UserFollower item in listFollower)
        //        {
        //            UserFollowerDto obj = new UserFollowerDto();

        //            obj.UserFollower = await GetSummaryById(item.UserId);
        //            obj.CreatedDate = item.CreatedDate;
        //            obj.UpdateDate = item.UpdateDate;

        //            response.Add(obj);
        //        }

        //    }

        //    return response;
        //}

        //private async Task<UserResponseDto> GetUserResponseForgotAsync(User? consult)
        //{
        //    UserResponseDto response = new UserResponseDto();

        //    if (consult != null)
        //    {
        //        response.Name = consult.Name;
        //        response.Identifier = consult.Identifier;
        //    }
        //    return response;
        //}

        //#endregion

    }
}
