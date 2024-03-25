using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SendGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SyspotecDal.Repository
{
    public class HibeatRepository : IHibeatRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HibeatRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<HibeatResponseDto>?> GetAll(PaginationDto pagination)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var queryable = _context.HiBeat
                 .AsNoTracking()
                 .Include("HiBeatInstrumentInterest")
                 .Include("HiBeatMusicalInterest")
                 .Include("User")
                 .Include("User.Subscription")
                 .Include("User.Gender")
                 .Include("User.State")
                 .Include("User.UserImage")
                 .Include("User.UserImage.State")
                 .Include("User.UserImage.TypeImage")
                 .Include("State")
                 .Include("Reaction")
                 .Include("Reaction.TypeReaction")
                 .Include("Reaction.State")
                 .Include("Reaction.User")
                 .Include("Reaction.User.Subscription")
                 .Include("Reaction.User.Gender")
                 .Include("Reaction.User.State")
                 .Include("Reaction.User.UserImage")
                 .Include("Reaction.User.UserImage.State")
                 .Include("Reaction.User.UserImage.TypeImage")
                 .Where(h => h.StateId == (int)StateEnum.Active)
                 .AsQueryable();

            await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

            var list = await queryable
                .OrderBy(c => Guid.NewGuid())
                .Paginate(pagination)
                .ToListAsync();

            lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseSummaryAsync(g)).ToList()!);

            return lstHibeatResponse;
        }

        public async Task<List<HibeatResponseDto>?> GetAllAdmin()
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var queryable = _context.HiBeat
                 .AsNoTracking()
                 .Include("HiBeatInstrumentInterest")
                 .Include("HiBeatMusicalInterest")
                 .Include("User")
                 .Include("User.Subscription")
                 .Include("User.Gender")
                 .Include("User.State")
                 .Include("User.UserImage")
                 .Include("User.UserImage.State")
                 .Include("User.UserImage.TypeImage")
                 .Include("State")
                 .Include("Reaction")
                 .Include("Reaction.TypeReaction")
                 .Include("Reaction.State")
                 .Include("Reaction.User")
                 .Include("Reaction.User.Subscription")
                 .Include("Reaction.User.Gender")
                 .Include("Reaction.User.State")
                 .Include("Reaction.User.UserImage")
                 .Include("Reaction.User.UserImage.State")
                 .Include("Reaction.User.UserImage.TypeImage")
                 .AsQueryable();

            var list = await queryable
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseSummaryAsync(g)).ToList()!);

            return lstHibeatResponse;
        }

        public async Task<List<HibeatResponseDto>?> GetByUserIdentifier(string identifier)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var consultUser = await GetUserByIdentifier(identifier);
            if (consultUser != null)
            {
                var list = _context.HiBeat
                   .AsNoTracking()
                   .Include("HiBeatInstrumentInterest")
                   .Include("HiBeatMusicalInterest")
                   .Include("User")
                   .Include("User.Subscription")
                   .Include("User.Gender")
                   .Include("User.State")
                   .Include("User.UserImage")
                   .Include("User.UserImage.State")
                   .Include("User.UserImage.TypeImage")
                   .Include("State")
                   .Include("Reaction")
                   .Include("Reaction.TypeReaction")
                   .Include("Reaction.State")
                   .Include("Reaction.User")
                   .Include("Reaction.User.Subscription")
                   .Include("Reaction.User.Gender")
                   .Include("Reaction.User.State")
                   .Include("Reaction.User.UserImage")
                   .Include("Reaction.User.UserImage.State")
                   .Include("Reaction.User.UserImage.TypeImage")
                   .Where(r => r.UserId == consultUser.Id && r.StateId == (int)StateEnum.Active)
                   .OrderBy(r => r.CreatedDate)
                   .ToList();

                if (list.Count > 0)
                {
                    lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseAsync(g)).ToList()!);
                }
            }

            return lstHibeatResponse;
        }

        public async Task<List<HibeatResponseDto>?> GetAllFilterByName(PaginationDto pagination, string name)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var queryable = _context.HiBeat
                 .AsNoTracking()
                 .Include("HiBeatInstrumentInterest")
                 .Include("HiBeatMusicalInterest")
                 .Include("User")
                 .Include("User.Subscription")
                 .Include("User.Gender")
                 .Include("User.State")
                 .Include("User.UserImage")
                 .Include("User.UserImage.State")
                 .Include("User.UserImage.TypeImage")
                 .Include("State")
                 .Include("Reaction")
                 .Include("Reaction.TypeReaction")
                 .Include("Reaction.State")
                 .Include("Reaction.User")
                 .Include("Reaction.User.Subscription")
                 .Include("Reaction.User.Gender")
                 .Include("Reaction.User.State")
                 .Include("Reaction.User.UserImage")
                 .Include("Reaction.User.UserImage.State")
                 .Include("Reaction.User.UserImage.TypeImage")
                 .Where(h => h.Title.ToLower().Contains(name!) && h.StateId == (int)StateEnum.Active)
                 .AsQueryable();

            await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

            var list = await queryable
                .Paginate(pagination)
                .ToListAsync();

            if (list.Count > 0)
            {
                lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseAsync(g)).ToList()!);
            }

            return lstHibeatResponse;
        }

        public async Task<List<HibeatResponseDto>?> GetAllFilterByMusicalInterest(PaginationDto pagination, MusicalInterestDto musicalInterest)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var queryable = _context.HiBeatMusicalInterest
                 .AsNoTracking()
                 .Include("HiBeat")
                 .Include("HiBeat.HiBeatInstrumentInterest")
                 .Include("HiBeat.User")
                 .Include("HiBeat.User.Subscription")
                 .Include("HiBeat.User.Gender")
                 .Include("HiBeat.User.State")
                 .Include("HiBeat.User.UserImage")
                 .Include("HiBeat.User.UserImage.State")
                 .Include("HiBeat.User.UserImage.TypeImage")
                 .Include("HiBeat.State")
                 .Include("HiBeat.Reaction")
                 .Include("HiBeat.Reaction.TypeReaction")
                 .Include("HiBeat.Reaction.State")
                 .Include("HiBeat.Reaction.User")
                 .Include("HiBeat.Reaction.User.Subscription")
                 .Include("HiBeat.Reaction.User.Gender")
                 .Include("HiBeat.Reaction.User.State")
                 .Include("HiBeat.Reaction.User.UserImage")
                 .Include("HiBeat.Reaction.User.UserImage.State")
                 .Include("HiBeat.Reaction.User.UserImage.TypeImage")
                 .Where(h => h.MusicalInterestId == musicalInterest.Id)
                 .AsQueryable();

            await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

            var list = await queryable
                .Paginate(pagination)
                .ToListAsync();

            if (list.Count > 0)
            {
                lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseAsync(g.HiBeat)).ToList()!);
            }

            return lstHibeatResponse;
        }

        public List<HibeatResponseDto> GetByUserId(int userId)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var list = _context.HiBeat
               .AsNoTracking()
               .Include("HiBeatInstrumentInterest")
               .Include("HiBeatMusicalInterest")
               .Include("User")
               .Include("User.Subscription")
               .Include("User.Gender")
               .Include("User.State")
               .Include("User.UserImage")
               .Include("User.UserImage.State")
               .Include("User.UserImage.TypeImage")
               .Include("User.UserMap")
               .Include("User.UserMap.State")
               .Include("State")
               .Include("Reaction")
               .Include("Reaction.TypeReaction")
               .Include("Reaction.State")
               .Include("Reaction.User")
               .Include("Reaction.User.Subscription")
               .Include("Reaction.User.Gender")
               .Include("Reaction.User.State")
               .Include("Reaction.User.UserImage")
               .Include("Reaction.User.UserImage.State")
               .Include("Reaction.User.UserImage.TypeImage")
               .Where(r => r.UserId == userId && r.StateId == (int)StateEnum.Active)
               .OrderBy(r => r.CreatedDate)
               .ToList();

            if (list.Count > 0)
            {
                lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseSummaryAsync(g)).ToList()!);
            }

            return lstHibeatResponse;
        }

        public HibeatResponseDto? GetByIdentifier(string identifier, int challengePoint = 0)
        {
            HibeatResponseDto response = new HibeatResponseDto();

            var consult = _context.HiBeat
               .AsNoTracking()
               .Include("HiBeatInstrumentInterest")
               .Include("HiBeatMusicalInterest")
               .Include("User")
               .Include("User.Subscription")
               .Include("User.Gender")
               .Include("User.State")
               .Include("User.UserImage")
               .Include("User.UserImage.State")
               .Include("User.UserImage.TypeImage")
               .Include("State")
               .Include("Reaction")
               .Include("Reaction.TypeReaction")
               .Include("Reaction.State")
               .Include("Reaction.User")
               .Include("Reaction.User.Subscription")
               .Include("Reaction.User.Gender")
               .Include("Reaction.User.State")
               .Include("Reaction.User.UserImage")
               .Include("Reaction.User.UserImage.State")
               .Include("Reaction.User.UserImage.TypeImage")
               .FirstOrDefault(r => r.Identifier == identifier && r.StateId == (int)StateEnum.Active);

            if (consult != null)
            {
                response = GetHibeatResponseAsync(consult, challengePoint > 0 ? challengePoint : 0);
            }

            return response.Identifier == null ? null : response;
        }

        public HibeatResponseDto? GetByIdentifierSummary(string identifier, int challengePoint = 0)
        {
            HibeatResponseDto response = new HibeatResponseDto();

            var consult = _context.HiBeat
               .AsNoTracking()
               .Include("HiBeatInstrumentInterest")
               .Include("HiBeatMusicalInterest")
               .Include("User")
               .Include("User.Subscription")
               .Include("User.Gender")
               .Include("User.State")
               .Include("User.UserImage")
               .Include("User.UserImage.State")
               .Include("User.UserImage.TypeImage")
               .Include("State")
               .Include("Reaction")
               .Include("Reaction.TypeReaction")
               .Include("Reaction.State")
               .Include("Reaction.User")
               .Include("Reaction.User.Subscription")
               .Include("Reaction.User.Gender")
               .Include("Reaction.User.State")
               .Include("Reaction.User.UserImage")
               .Include("Reaction.User.UserImage.State")
               .Include("Reaction.User.UserImage.TypeImage")
               .FirstOrDefault(r => r.Identifier == identifier && r.StateId == (int)StateEnum.Active);

            if (consult != null)
            {
                response = GetHibeatResponseSummaryAsync(consult, challengePoint > 0 ? challengePoint : 0);
            }

            return response.Identifier == null ? null : response;
        }

        public async Task<HiBeat?> GetIdByIdentifier(string identifier)
        {
            var obj = await _context.HiBeat.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier);
            return obj;
        }

        public async Task<HiBeat?> GetById(int id)
        {
            var obj = await _context.HiBeat.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return obj;
        }

        public async Task<HiBeat?> GetByTitle(string title)
        {
            var obj = await _context.HiBeat.AsNoTracking().FirstOrDefaultAsync(c => c.Title == title);
            return obj;
        }

        public async Task<HiBeat?> GetIsValid(int userId, HiBeatDto request)
        {
            var obj = await _context.HiBeat
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.UserId == userId &&
                    c.StateId == (int)StateEnum.Active &&
                    c.Title == request.Title
                );

            return obj;
        }

        public async Task<int?> Add(HiBeat model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(HiBeat model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<HibeatResponseDto>?> GetTopLastWeek()
        {
            List<HibeatResponseDto> lstHibeat = new List<HibeatResponseDto>();
            List<HiBeatTop> lstHibeatSql = new List<HiBeatTop>();

            lstHibeatSql = await _context.HiBeatTop.FromSqlRaw("TopHbLastWeek").ToListAsync();
            if (lstHibeatSql.Count > 0)
            {
                lstHibeat.AddRange(lstHibeatSql.AsEnumerable().Select(g => GetByIdentifierSummary(g.Identifier, g.Points)).ToList()!);
            }

            return lstHibeat;
        }

        public List<ReactionResponseDto> GetAllNotificationByUser(int userId)
        {
            List<ReactionResponseDto> lstResponse = new List<ReactionResponseDto>();

            var list = _context.HiBeat
                 .AsNoTracking()
                 .Include("HiBeatInstrumentInterest")
                 .Include("HiBeatMusicalInterest")
                 .Include("User")
                 .Include("User.Subscription")
                 .Include("User.Gender")
                 .Include("User.State")
                 .Include("User.UserImage")
                 .Include("User.UserImage.State")
                 .Include("User.UserImage.TypeImage")
                 .Include("State")
                 .Include(x => x.Reaction.Where(
                     y => y.CreatedDate.Year.ToString() + y.CreatedDate.Month.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                     && (y.TypeReactionId == (int)TypeReactionEnum.Like || y.TypeReactionId == (int)TypeReactionEnum.Comment)
                  ))
                 .Include("Reaction.TypeReaction")
                 .Include("Reaction.State")
                 .Include("Reaction.User")
                 .Include("Reaction.User.Subscription")
                 .Include("Reaction.User.Gender")
                 .Include("Reaction.User.State")
                 .Include("Reaction.User.UserImage")
                 .Include("Reaction.User.UserImage.State")
                 .Include("Reaction.User.UserImage.TypeImage")
                 .Where(r => r.UserId == userId && r.StateId == (int)StateEnum.Active)
                 .OrderBy(r => r.CreatedDate)
                 .ToList();

            if (list.Count > 0)
            {
                foreach (HiBeat item in list)
                {
                    if (item.Reaction.Count > 0)
                    {
                        var consultReactions = GetReactionNotification(item.Reaction.ToList(), item);
                        if (consultReactions.Count > 0)
                        {
                            lstResponse.AddRange(consultReactions);
                        }
                    }

                }
            }

            return lstResponse;
        }

        #region Challenge

        private async Task<List<HibeatResponseDto>> GetChallengeHibeatAsync(int challengeId, PaginationDto pagination)
        {
            List<HibeatResponseDto> response = new List<HibeatResponseDto>();

            if (challengeId > 0)
            {
                var queryable = _context.ChallengeHiBeat
                   .AsNoTracking()
                   .Include("HiBeat")
                   .Include("HiBeat.HiBeatInstrumentInterest")
                   .Include("HiBeat.HiBeatMusicalInterest")
                   .Include("HiBeat.User")
                   .Include("HiBeat.User.Subscription")
                   .Include("HiBeat.User.Gender")
                   .Include("HiBeat.User.State")
                   .Include("HiBeat.User.UserImage")
                   .Include("HiBeat.User.UserImage.State")
                   .Include("HiBeat.User.UserImage.TypeImage")
                   .Include("HiBeat.State")
                   .Include("HiBeat.Reaction")
                   .Include("HiBeat.Reaction.TypeReaction")
                   .Include("HiBeat.Reaction.State")
                   .Include("HiBeat.Reaction.User")
                   .Include("HiBeat.Reaction.User.Subscription")
                   .Include("HiBeat.Reaction.User.Gender")
                   .Include("HiBeat.Reaction.User.State")
                   .Include("HiBeat.Reaction.User.UserImage")
                   .Include("HiBeat.Reaction.User.UserImage.State")
                   .Include("HiBeat.Reaction.User.UserImage.TypeImage")
                   .Where(ch => ch.ChallengeId == challengeId)
                   .AsQueryable();

                //await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

                var list = await queryable
                    .OrderByDescending(c => c.Points)
                    .Paginate(pagination)
                    .ToListAsync();

                response.AddRange(list.AsEnumerable().Select(g => GetByIdentifierSummary(g.HiBeat.Identifier, g.Points)).ToList()!);
            }

            return response;
        }

        #endregion

        #region Feed

        public async Task<HibeatResponseDto?> GetFeed(int userId)
        {
            var consult = await _context.HiBeat
               .AsNoTracking()
               .Include("HiBeatInstrumentInterest")
               .Include("HiBeatMusicalInterest")
               .Include("User")
               .Include("User.Subscription")
               .Include("User.Gender")
               .Include("User.State")
               .Include("User.UserImage")
               .Include("User.UserImage.State")
               .Include("User.UserImage.TypeImage")
               .Include("State")
               .Include("Reaction")
               .Include("Reaction.TypeReaction")
               .Include("Reaction.State")
               .Include("Reaction.User")
               .Include("Reaction.User.Subscription")
               .Include("Reaction.User.Gender")
               .Include("Reaction.User.State")
               .Include("Reaction.User.UserImage")
               .Include("Reaction.User.UserImage.State")
               .Include("Reaction.User.UserImage.TypeImage")
               .OrderByDescending(r => r.CreatedDate)
               .FirstOrDefaultAsync(r => r.UserId == userId && r.StateId == (int)StateEnum.Active);

            if (consult != null)
            {
                return GetHibeatResponseSummaryAsync(consult);
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Play List

        public async Task<List<PlayListResponseDto>?> GetAllPlayList(PlayListDto pagination)
        {
            List<PlayListResponseDto> lstResponse = new List<PlayListResponseDto>();
            PlayList? consultPlayList = new PlayList();

            if (pagination.Filter == "all")
            {
                var list = await _context.PlayList
                                                .Include("State")
                                                .Include("TypePlayList")
                                                .Where(p => p.StateId == (int)StateEnum.Active)
                                                .OrderBy(c => c.Id)
                                                .ToListAsync();
                if (list.Count > 0)
                {
                    foreach (PlayList item in list)
                    {
                        lstResponse.Add(await PlayListDto(pagination, item));
                    }
                }
            }
            else if (pagination.Filter == "challenge")
            {
                var list = await _context.PlayList
                                               .Include("State")
                                               .Include("TypePlayList")
                                               .Where(p => p.StateId == (int)StateEnum.Active && p.TypePlayList.Name == "challenge")
                                               .OrderBy(c => c.Id)
                                               .ToListAsync();
                if (list.Count > 0)
                {
                    foreach (PlayList item in list)
                    {
                        lstResponse.Add(await PlayListDto(pagination, item));
                    }
                }
            }
            else
            {
                consultPlayList = await _context.PlayList
                                            .Include("State")
                                            .Include("TypePlayList")
                                            .FirstOrDefaultAsync(p => p.StateId == (int)StateEnum.Active && p.TypePlayList.Name == pagination.Filter);
                if (consultPlayList != null)
                {
                    lstResponse.Add(await PlayListDto(pagination, consultPlayList));
                }
            }


            return lstResponse;
        }

        private async Task<PlayListResponseDto> PlayListDto(PlayListDto pagination, PlayList playList)
        {
            List<HibeatResponseDto> lstHibeat = new List<HibeatResponseDto>();
            List<HiBeat> lstHibeatSql = new List<HiBeat>();
            PlayListResponseDto response = new PlayListResponseDto();
            TypePlayListDto objTypePlayList = new TypePlayListDto();
            StateDto objState = new StateDto();

            if (playList.TypePlayList != null)
            {
                if (playList.TypePlayList.Name == "top" || playList.TypePlayList.Name == "recent")
                {
                    if (playList.TypePlayList.Name == "top")
                    {
                        lstHibeatSql = await _context.HiBeat.FromSqlRaw("ConsultHibeatsTop").ToListAsync();
                    }
                    else
                    {
                        lstHibeatSql = await _context.HiBeat.FromSqlRaw("ConsultHibeatsRecent").ToListAsync();
                    }
                    lstHibeat.AddRange(lstHibeatSql.AsEnumerable().Select(g => GetByIdentifierSummary(g.Identifier)).ToList()!);
                }
                else if (playList.TypePlayList.Name == "map")
                {
                    var consultUsersMap = _context.UserMap.AsNoTracking().AsQueryable();

                    var listHb = await consultUsersMap
                      .OrderBy(c => c.CreatedDate)
                      .Where(r => r.StateId == (int)StateEnum.Active)
                      .Paginate(pagination)
                      .ToListAsync();

                    if (listHb.Count > 0)
                    {
                        foreach (UserMap user in listHb)
                        {
                            lstHibeat.AddRange(GetByUserId(user.UserId));
                        }
                    }
                }
                else if (playList.TypePlayList.Name == "challenge")
                {
                    var consultChallenge = await _context.Challenge.AsNoTracking().FirstOrDefaultAsync(ch => ch.PlayListId == playList.Id);
                    if (consultChallenge != null)
                    {
                        lstHibeat.AddRange(await GetChallengeHibeatAsync(consultChallenge.Id, pagination));
                    }
                }

                //Maping
                objState.Id = playList.State.Id;
                objState.Name = playList.State.Name;

                objTypePlayList.Id = playList.TypePlayList.Id;
                objTypePlayList.Name = playList.TypePlayList.Name;

                response.TypePlayList = objTypePlayList;
                response.State = objState;
                response.lstHibeat = lstHibeat;
                response.CoverImage = playList.CoverImage;
                response.Title = playList.Title;
                response.Description = playList.Description;
                response.CreatedDate = playList.CreatedDate;
                response.UpdateDate = playList.UpdateDate;
            }

            return response;
        }

        #endregion

        #region My Likes

        public async Task<List<HibeatResponseDto>?> GetByLike(string identifier, PaginationDto pagination)
        {
            List<HibeatResponseDto> lstHibeatResponse = new List<HibeatResponseDto>();

            var consultUser = await GetUserByIdentifier(identifier);
            if (consultUser != null)
            {
                var queryable = _context.Reaction
                                    .AsNoTracking()
                                    .Include("TypeReaction")
                                    .Include("State")
                                    .Include("User")
                                    .Include("User.Subscription")
                                    .Include("User.Gender")
                                    .Include("User.State")
                                    .Include("User.UserImage")
                                    .Include("User.UserImage.State")
                                    .Include("User.UserImage.TypeImage")
                                    .Include("HiBeat")
                                    .Include("HiBeat.State")
                                    .Include("HiBeat.HiBeatInstrumentInterest")
                                    .Include("HiBeat.HiBeatMusicalInterest")
                                    .Include("HiBeat.User")
                                    .Include("HiBeat.User.Subscription")
                                    .Include("HiBeat.User.Gender")
                                    .Include("HiBeat.User.State")
                                    .Include("HiBeat.User.UserImage")
                                    .Include("HiBeat.User.UserImage.State")
                                    .Include("HiBeat.User.UserImage.TypeImage")
                                    .Include("State")
                                    .Where(r => r.UserId == consultUser.Id && r.TypeReactionId == (int)TypeReactionEnum.Like && r.StateId == (int)StateEnum.Active)
                                    .OrderByDescending(r => r.CreatedDate)
                                    .AsQueryable();

                await _httpContextAccessor.HttpContext.InsertPaginationHeader(queryable);

                var list = await queryable
                       .OrderByDescending(c => c.CreatedDate)
                       .Paginate(pagination)
                       .ToListAsync();

                if (list.Count > 0)
                {
                    lstHibeatResponse.AddRange(list.AsEnumerable().Select(g => GetHibeatResponseSummaryAsync(g.HiBeat)).ToList()!);
                }
            }

            return lstHibeatResponse;
        }

        #endregion

        #region Map Hibeats

        public async Task<List<UserResponseSummaryDto>?> GetAllByMap()
        {
            List<UserResponseSummaryDto> lstUser = new List<UserResponseSummaryDto>();

            var listHb = await _context.UserMap
                                .AsNoTracking()
                                .Include("State")
                                .Include("User")
                                .Include("User.Subscription")
                                .Include("User.Gender")
                                .Include("User.State")
                                .Include("User.UserImage")
                                .Include("User.UserImage.State")
                                .Include("User.UserImage.TypeImage")
                                .Where(r => r.StateId == (int)StateEnum.Active)
                                .OrderBy(c => c.CreatedDate)
                                .ToListAsync();

            if (listHb.Count > 0)
            {
                lstUser.AddRange(listHb.AsEnumerable().Select(g => GetUserResponseSummary(g.User)).ToList()!);
            }

            return lstUser;
        }

        #endregion

        #region Hibeat Data DTO

        public HibeatResponseDto GetHibeatResponseAsync(HiBeat? consult, int challengePoint = 0)
        {
            HibeatResponseDto response = new HibeatResponseDto();

            if (consult != null)
            {
                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.User = GetUserResponseSummary(consult.User);
                response.State = objState;

                response.Title = consult.Title.Trim();
                response.Tone = consult.Tone;
                response.Duration = consult.Duration;
                response.Bpm = consult.Bpm;
                response.RecordCompany = consult.RecordCompany.Trim();
                response.UrlFile = consult.UrlFile;
                response.UrlCover = consult.UrlCover;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;

                response.LstInstrument = GetHiBeatInstrumentInterest(consult.HiBeatInstrumentInterest.ToList());
                response.LstMusicalInterest = GetHiBeatMusicalInterest(consult.HiBeatMusicalInterest.ToList());

                List<Reaction> lstReaction = new List<Reaction>();

                //calc points
                var consultReaction = GetReactionByHibeatIdentifier(consult.Reaction == null ? lstReaction : consult.Reaction.ToList());
                var sumPoint = consultReaction.Sum(r => r.TypeReaction.Point);

                response.LstReaction = consultReaction;
                response.Points = challengePoint > 0 ? challengePoint : sumPoint;

                //stats
                HiBeatStatsDto stats = new HiBeatStatsDto();

                stats.TotalMonthlyListener = consultReaction
                                            .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction &&
                                                   r.CreatedDate.Year.ToString() + r.CreatedDate.Month.ToString() == DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                                             )
                                            .Count();

                stats.TotalReproduction = consultReaction.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction).Count();
                stats.TotalLike = consultReaction.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Like).Count();
                stats.TotalShare = consultReaction.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Share).Count();
                stats.TotalComment = consultReaction.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Comment).Count();

                stats.LstContry = consultReaction
                                .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction)
                                .GroupBy(g => g.User.Nationality)
                                .Select(g => new HiBeatStatsCountryDto { Name = g.Key, Value = g.Count() })
                                .ToList();

                stats.LstAge = GetAgeGraphic(consultReaction);
                stats.LstGender = GetGenderGraphic(consultReaction);

                response.Stats = stats;
            }

            return response;
        }

        public HibeatResponseDto GetHibeatResponseSummaryAsync(HiBeat? consult, int challengePoint = 0)
        {
            HibeatResponseDto response = new HibeatResponseDto();

            if (consult != null)
            {
                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.User = GetUserResponseSummary(consult.User);
                response.State = objState;

                response.Title = consult.Title.Trim();
                response.Tone = consult.Tone;
                response.Duration = consult.Duration;
                response.Bpm = consult.Bpm;
                response.RecordCompany = consult.RecordCompany.Trim();
                response.UrlFile = consult.UrlFile;
                response.UrlCover = consult.UrlCover;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;

                response.LstInstrument = GetHiBeatInstrumentInterest(consult.HiBeatInstrumentInterest.ToList());
                response.LstMusicalInterest = GetHiBeatMusicalInterest(consult.HiBeatMusicalInterest.ToList());

                List<Reaction> lstReaction = new List<Reaction>();

                //calc points
                var consultReaction = GetReactionByHibeatIdentifier(consult.Reaction == null ? lstReaction : consult.Reaction.ToList());
                var sumPoint = consultReaction.Sum(r => r.TypeReaction.Point);

                response.LstReaction = consultReaction;
                response.Points = challengePoint > 0 ? challengePoint : sumPoint;
            }

            return response;
        }

        private HibeatResponseDto GetHibeatResponseNotification(HiBeat? consult)
        {
            HibeatResponseDto response = new HibeatResponseDto();

            if (consult != null)
            {
                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.User = GetUserResponseSummary(consult.User);
                response.State = objState;

                response.Title = consult.Title.Trim();
                response.Tone = consult.Tone;
                response.Duration = consult.Duration;
                response.Bpm = consult.Bpm;
                response.RecordCompany = consult.RecordCompany.Trim();
                response.UrlFile = consult.UrlFile;
                response.UrlCover = consult.UrlCover;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;

                response.LstInstrument = GetHiBeatInstrumentInterest(consult.HiBeatInstrumentInterest.ToList());
                response.LstMusicalInterest = GetHiBeatMusicalInterest(consult.HiBeatMusicalInterest.ToList());

                List<Reaction> lstReaction = new List<Reaction>();

                var consultReaction = GetReactionByHibeatIdentifier(consult.Reaction == null ? lstReaction : consult.Reaction.ToList());
                var sumPoint = consultReaction.Sum(r => r.TypeReaction.Point);

                response.Points = sumPoint;
            }

            return response;
        }

        private List<HiBeatStatsAgeDto> GetAgeGraphic(List<ReactionResponseDto>? consult)
        {
            List<HiBeatStatsAgeDto> response = new List<HiBeatStatsAgeDto>();

            if (consult != null)
            {
                var amount = consult.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction).Count();

                var totalFirst = consult
                               .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && (DateTime.Now.Year - r.User.BirthDate.Year) <= 17)
                               .GroupBy(g => g.User.Identifier)
                               .Select(g => new { Value = g.Count() })
                               .Sum(g => g.Value);

                double calcFirst = ((double)totalFirst / (double)amount) * 100;
                response.Add(new HiBeatStatsAgeDto { Name = "0-17", Value = (int)calcFirst });

                var totalSecond = consult
                              .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && ((DateTime.Now.Year - r.User.BirthDate.Year) >= 18 && (DateTime.Now.Year - r.User.BirthDate.Year) <= 22))
                              .GroupBy(g => g.User.Identifier)
                              .Select(g => new { Value = g.Count() })
                              .Sum(g => g.Value);

                double calcSecond = ((double)totalSecond / (double)amount) * 100;
                response.Add(new HiBeatStatsAgeDto { Name = "18-22", Value = (int)calcSecond });

                var totalThird = consult
                              .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && ((DateTime.Now.Year - r.User.BirthDate.Year) >= 23 && (DateTime.Now.Year - r.User.BirthDate.Year) <= 27))
                              .GroupBy(g => g.User.Identifier)
                              .Select(g => new { Value = g.Count() })
                              .Sum(g => g.Value);

                double calcThird = ((double)totalThird / (double)amount) * 100;
                response.Add(new HiBeatStatsAgeDto { Name = "23-27", Value = (int)calcThird });

                var totalFourth = consult
                              .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && ((DateTime.Now.Year - r.User.BirthDate.Year) >= 28 && (DateTime.Now.Year - r.User.BirthDate.Year) <= 34))
                              .GroupBy(g => g.User.Identifier)
                              .Select(g => new { Value = g.Count() })
                              .Sum(g => g.Value);

                double calcFourth = ((double)totalFourth / (double)amount) * 100;
                response.Add(new HiBeatStatsAgeDto { Name = "28-34", Value = (int)calcFourth });

                var totalFifth = consult
                              .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && (DateTime.Now.Year - r.User.BirthDate.Year) >= 35)
                              .GroupBy(g => g.User.Identifier)
                              .Select(g => new { Value = g.Count() })
                              .Sum(g => g.Value);

                double calcFifth = ((double)totalFifth / (double)amount) * 100;
                response.Add(new HiBeatStatsAgeDto { Name = "35+", Value = (int)calcFifth });
            }

            return response;
        }

        private List<HiBeatStatsGenderDto> GetGenderGraphic(List<ReactionResponseDto>? consult)
        {
            List<HiBeatStatsGenderDto> response = new List<HiBeatStatsGenderDto>();

            if (consult != null)
            {
                var amount = consult.Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction).Count();

                var totalFemale = consult
                               .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && r.User.Gender.Id == (int)GenderEnum.Female)
                               .GroupBy(g => g.User.Identifier)
                               .Select(g => new { Value = g.Count() })
                               .Sum(g => g.Value);

                double calcFemale = ((double)totalFemale / (double)amount) * 100;
                response.Add(new HiBeatStatsGenderDto { Name = "Femenino", Value = (int)calcFemale });

                var totalMale = consult
                               .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && r.User.Gender.Id == (int)GenderEnum.Male)
                               .GroupBy(g => g.User.Identifier)
                               .Select(g => new { Value = g.Count() })
                               .Sum(g => g.Value);

                double calcMale = ((double)totalMale / (double)amount) * 100;
                response.Add(new HiBeatStatsGenderDto { Name = "Masculino", Value = (int)calcMale });

                var totalNonBinary = consult
                           .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && r.User.Gender.Id == (int)GenderEnum.NonBinary)
                           .GroupBy(g => g.User.Identifier)
                           .Select(g => new { Value = g.Count() })
                           .Sum(g => g.Value);

                double calcNonBinary = ((double)totalNonBinary / (double)amount) * 100;
                response.Add(new HiBeatStatsGenderDto { Name = "No Binario", Value = (int)calcNonBinary });

                var totalNotSpecified = consult
                           .Where(r => r.TypeReaction.Id == (int)TypeReactionEnum.Reproduction && r.User.Gender.Id == (int)GenderEnum.NotSpecified)
                           .GroupBy(g => g.User.Identifier)
                           .Select(g => new { Value = g.Count() })
                           .Sum(g => g.Value);

                double calcNotSpecified = ((double)totalNotSpecified / (double)amount) * 100;
                response.Add(new HiBeatStatsGenderDto { Name = "No Especificado", Value = (int)calcNotSpecified });
            }

            return response;
        }

        #region User DTO

        private async Task<User?> GetUserByIdentifier(string id)
        {
            var obj = await _context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Identifier == id);

            return obj;
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

                        //State
                        StateDto objStateImage = new StateDto();
                        objStateImage.Id = item.State.Id;
                        objStateImage.Name = item.State.Name;

                        //Type Image
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

        private UserResponseSummaryDto GetUserResponseSummary(User? consult)
        {
            UserResponseSummaryDto response = new UserResponseSummaryDto();

            if (consult != null)
            {
                //Subscription
                var consultTypeSubscription = _context.TypeSubscription.AsNoTracking().FirstOrDefault(u => u.Id == consult.Subscription.TypeSubscriptionId);
                TypeSubscriptionDto objTypeSubscription = new TypeSubscriptionDto();

                if (consultTypeSubscription != null)
                {
                    objTypeSubscription.Id = consultTypeSubscription.Id;
                    objTypeSubscription.Name = consultTypeSubscription.Name;
                }

                SubscriptionDto objSubscription = new SubscriptionDto();
                objSubscription.Id = consult.Subscription.Id;
                //objSubscription.TypeSubscriptionId = consult.Subscription.TypeSubscriptionId;
                objSubscription.TypeSubscription = objTypeSubscription;
                objSubscription.Name = consult.Subscription.Name;
                objSubscription.Price = consult.Subscription.Price;
                objSubscription.CreatedDate = consult.Subscription.CreatedDate;
                objSubscription.UpdateDate = consult.Subscription.UpdateDate;

                //Gender
                GenderDto objGender = new GenderDto();
                objGender.Id = consult.Gender.Id;
                objGender.Name = consult.Gender.Name;

                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                //response.SubscriptionId = consult.SubscriptionId;
                response.Subscription = objSubscription;
                //response.GenderId = consult.GenderId;
                response.Gender = objGender;
                //response.StateId = consult.StateId;
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
                response.Map = consult.UserMap != null ? GetMap(consult.UserMap.ToList()) : null;

                response.ListImage = GetAllImageUser(consult.UserImage.ToList());
            }

            return response;
        }

        private UserMapDto? GetMap(List<UserMap> consult)
        {
            UserMapDto? response = new UserMapDto();
            List<UserMapDto> lst = new List<UserMapDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (UserMap item in consult)
                    {
                        UserMapDto obj = new UserMapDto();

                        //State
                        StateDto objStateMap = new StateDto();
                        objStateMap.Id = item.State.Id;
                        objStateMap.Name = item.State.Name;

                        obj.State = objStateMap;
                        obj.Latitude = item.Latitude;
                        obj.Longitude = item.Longitude;
                        obj.CreatedDate = item.CreatedDate;
                        obj.UpdateDate = item.UpdateDate;

                        lst.Add(obj);
                    }
                }
            }

            if (lst.Count > 0)
            {
                return response = lst.OrderBy(c => c.CreatedDate).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region HiBeatInstrumentInterest

        private List<InstrumentInterestDto> GetHiBeatInstrumentInterest(List<HiBeatInstrumentInterest> consult)
        {
            List<InstrumentInterestDto> response = new List<InstrumentInterestDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (HiBeatInstrumentInterest item in consult)
                    {
                        //InstrumentInterest
                        var consultInstrumentInterest = _context.InstrumentInterest.AsNoTracking().FirstOrDefault(u => u.Id == item.InstrumentInterestId);
                        InstrumentInterestDto objInstrumentInterest = new InstrumentInterestDto();

                        if (consultInstrumentInterest != null)
                        {
                            objInstrumentInterest.Id = consultInstrumentInterest.Id;
                            objInstrumentInterest.Name = consultInstrumentInterest.Name;

                            response.Add(objInstrumentInterest);
                        }
                    }
                }

            }

            return response;
        }

        #endregion

        #region HiBeatMusicalInterest

        private List<MusicalInterestDto> GetHiBeatMusicalInterest(List<HiBeatMusicalInterest> consult)
        {
            List<MusicalInterestDto> response = new List<MusicalInterestDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (HiBeatMusicalInterest item in consult)
                    {
                        //MusicalInterest
                        var consultMusicalInterest = _context.MusicalInterest.AsNoTracking().FirstOrDefault(u => u.Id == item.MusicalInterestId);
                        MusicalInterestDto objMusicalInterest = new MusicalInterestDto();

                        if (consultMusicalInterest != null)
                        {
                            objMusicalInterest.Id = consultMusicalInterest.Id;
                            objMusicalInterest.Name = consultMusicalInterest.Name;

                            response.Add(objMusicalInterest);
                        }
                    }
                }
            }

            return response;
        }

        #endregion

        #region Reaction

        public List<ReactionResponseDto> GetReactionByHibeatIdentifier(List<Reaction> reactions)
        {
            List<ReactionResponseDto> lstReactionResponse = new List<ReactionResponseDto>();

            if (reactions.Count > 0)
            {
                lstReactionResponse.AddRange(reactions.AsEnumerable().Select(g => GetReactionResponseAsync(g)).ToList()!);
            }

            return lstReactionResponse;
        }

        public ReactionResponseDto GetReactionResponseAsync(Reaction? consult)
        {
            ReactionResponseDto response = new ReactionResponseDto();

            if (consult != null)
            {
                //TypeReaction
                TypeReactionDto objTypeReaction = new TypeReactionDto();
                objTypeReaction.Id = consult.TypeReaction.Id;
                objTypeReaction.Name = consult.TypeReaction.Name;
                objTypeReaction.Point = consult.TypeReaction.Point;

                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                //User
                response.User = GetUserResponseSummary(consult.User);

                response.TypeReaction = objTypeReaction;
                response.State = objState;
                response.Description = consult.Description.Trim();
                response.IsRead = consult.IsRead;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
            }

            return response;
        }

        private List<ReactionResponseDto> GetReactionNotification(List<Reaction> reactions, HiBeat hb)
        {
            List<ReactionResponseDto> lstReactionResponse = new List<ReactionResponseDto>();

            if (reactions.Count > 0)
            {
                lstReactionResponse.AddRange(reactions.AsEnumerable().Select(g => GetReactionNotificationResponse(g, hb)).ToList()!);
            }

            return lstReactionResponse;
        }

        private ReactionResponseDto GetReactionNotificationResponse(Reaction? consult, HiBeat hb)
        {
            ReactionResponseDto response = new ReactionResponseDto();

            if (consult != null)
            {
                //TypeReaction
                TypeReactionDto objTypeReaction = new TypeReactionDto();
                objTypeReaction.Id = consult.TypeReaction.Id;
                objTypeReaction.Name = consult.TypeReaction.Name;
                objTypeReaction.Point = consult.TypeReaction.Point;

                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                //User
                response.User = GetUserResponseSummary(consult.User);

                //HiBeat
                response.HiBeat = GetHibeatResponseNotification(hb);

                response.Id = consult.Id;
                response.TypeReaction = objTypeReaction;
                response.State = objState;
                response.Description = consult.Description.Trim();
                response.IsRead = consult.IsRead;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
            }

            return response;
        }

        #endregion


        #endregion

    }
}
