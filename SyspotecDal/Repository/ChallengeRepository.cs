using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Challenge;
using Microsoft.EntityFrameworkCore;
using SyspotecUtils;
using SyspotecDomain.Enums;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.IRepositories;

namespace SyspotecDal.Repository
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHibeatRepository _hibeatRepository;

        public ChallengeRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IHibeatRepository hibeatRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hibeatRepository = hibeatRepository;
        }

        public async Task<List<ChallengeResponseDto>?> GetAll(PaginationDto pagination)
        {
            List<ChallengeResponseDto> lstResponse = new List<ChallengeResponseDto>();

            var list = await _context.Challenge
                 .AsNoTracking()
                 .Include("State")
                 .Include("ChallengeAward")
                 .Include("ChallengeWinner")
                 .Include("ChallengeWinner.HiBeat")
                 .Include("ChallengeWinner.HiBeat.HiBeatInstrumentInterest")
                 .Include("ChallengeWinner.HiBeat.HiBeatMusicalInterest")
                 .Include("ChallengeWinner.HiBeat.User")
                 .Include("ChallengeWinner.HiBeat.User.Subscription")
                 .Include("ChallengeWinner.HiBeat.User.Gender")
                 .Include("ChallengeWinner.HiBeat.User.State")
                 .Include("ChallengeWinner.HiBeat.User.UserImage")
                 .Include("ChallengeWinner.HiBeat.User.UserImage.State")
                 .Include("ChallengeWinner.HiBeat.User.UserImage.TypeImage")
                 .Include("ChallengeWinner.HiBeat.State")
                 .Include("ChallengeWinner.HiBeat.Reaction")
                 .Include("ChallengeWinner.HiBeat.Reaction.TypeReaction")
                 .Include("ChallengeWinner.HiBeat.Reaction.State")
                 .Include("ChallengeWinner.HiBeat.Reaction.User")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.Subscription")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.Gender")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.State")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.UserImage")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.UserImage.State")
                 .Include("ChallengeWinner.HiBeat.Reaction.User.UserImage.TypeImage")
                 //.Where(ch => ch.StateId == (int)StateEnum.Active)
                 .OrderBy(c => c.CreatedDate)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (Challenge challenge in list)
                {

                    //deactivate challenge when you're done:
                    //  1). Desactivar el challenge
                    //  2). Activar la play list
                    //  3). Actualizar los puntos en la tabla ChallengeHiBeat. 

                    if (challenge.StateId == (int)StateEnum.Active)
                    {
                        if (challenge.EndDate < DateTime.Now)
                        {
                            //* deactivate challenge when you're done:
                        }
                    }

                    lstResponse.Add(await GetChallengeResponse(challenge, pagination));
                }
            }

            return lstResponse;
        }

        public async Task<List<ChallengeDto>?> GetAllSummary()
        {
            List<ChallengeDto> response = new List<ChallengeDto>();

            var consult = await _context.Challenge.AsNoTracking().Where(ch => ch.StateId == (int)StateEnum.Active).OrderBy(c => c.CreatedDate).ToListAsync();
            response = consult.Select(g => new ChallengeDto { 
                Title = g.Title.Trim(), 
                UrlImage = g.UrlImage,
                Description = g.Description.Trim(),
                LegalBases = g.LegalBases,
                StartDate = g.StartDate,
                EndDate = g.EndDate,
                CreatedDate = g.CreatedDate,
                UpdateDate = g.UpdateDate
            }).ToList();

            return response;
        }

        public async Task<List<HibeatResponseDto>> GetChallengeHibeatAsync(Challenge challenge, PaginationDto pagination)
        {
            List<HibeatResponseDto> response = new List<HibeatResponseDto>();

            if (challenge != null)
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
                   .Where(ch => ch.ChallengeId == challenge.Id)
                   .AsQueryable();

                var list = await queryable
                    .Paginate(pagination)
                    .ToListAsync();

                if (list.Count > 0)
                {
                    response.AddRange(list.AsEnumerable().Select(g => _hibeatRepository.GetHibeatResponseSummaryAsync(g.HiBeat, challenge.StateId == (int)StateEnum.Inactive ? g.Points : 0)).ToList()!);
                }
            }

            return response;
        }

        private async Task<ChallengeResponseDto> GetChallengeResponse(Challenge? consult, PaginationDto pagination)
        {
            ChallengeResponseDto response = new ChallengeResponseDto();

            if (consult != null)
            {
                //State
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Title = consult.Title.Trim();
                response.UrlImage = consult.UrlImage;
                response.Description = consult.Description.Trim();
                response.LegalBases = consult.LegalBases;
                response.StartDate = consult.StartDate;
                response.EndDate = consult.EndDate;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
                response.State = objState;

                response.LstAward = GetChallengeAward(consult.ChallengeAward.ToList());
                response.LstWinner = GetChallengeWinnerAsync(consult.ChallengeWinner.ToList());
                response.LstHibeat = await GetChallengeHibeatAsync(consult, pagination);
            }

            return response;
        }

        private List<ChallengeAwardDto> GetChallengeAward(List<ChallengeAward> consult)
        {
            List<ChallengeAwardDto> response = new List<ChallengeAwardDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (ChallengeAward item in consult)
                    {
                        ChallengeAwardDto objChallengeAward = new ChallengeAwardDto();
                        objChallengeAward.UrlImage = item.UrlImage;

                        response.Add(objChallengeAward);
                    }
                }
            }

            return response;
        }

        private List<ChallengeWinnerDto> GetChallengeWinnerAsync(List<ChallengeWinner> consult)
        {
            List<ChallengeWinnerDto> response = new List<ChallengeWinnerDto>();

            if (consult != null)
            {
                if (consult.Count > 0)
                {
                    foreach (ChallengeWinner item in consult)
                    {
                        ChallengeWinnerDto objChallengeWinner = new ChallengeWinnerDto();
                        objChallengeWinner.Hibeat = _hibeatRepository.GetHibeatResponseSummaryAsync(item.HiBeat);
                        objChallengeWinner.Position = item.Position;
                        objChallengeWinner.Points = item.Points;

                        response.Add(objChallengeWinner);
                    }
                }
            }

            return response;
        }

    }
}
