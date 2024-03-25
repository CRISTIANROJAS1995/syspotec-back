using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System.Text;
using System.Security.Cryptography;
using SyspotecDomain.Enums;
using SyspotecDomain.Dtos.User;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;

namespace SyspotecApplication.Services
{
    public class HomeService : IHomeService
    {
        private readonly ISendEmailRepository _sendEmailRepository;
        private readonly IUserService _userService;
        private readonly IHibeatService _hibeatService;
        private readonly IUserFollowerService _userFollowerService;
        private readonly IReactionService _reactionService;

        public HomeService(IUserService userService, IHibeatService hibeatService, IUserFollowerService userFollowerService, IReactionService reactionService, ISendEmailRepository sendEmailRepository)
        {
            _userService = userService;
            _hibeatService = hibeatService;
            _userFollowerService = userFollowerService;
            _reactionService = reactionService;
            _sendEmailRepository = sendEmailRepository;
        }

        public async Task<List<dynamic>?> SearchData(PaginationDto pagination, HiBeatFilterDto request)
        {
            List<dynamic> data = new List<dynamic>();

            if (request.Hibeat != string.Empty)
            {
                var consultHibeat = await _hibeatService.GetAllFilterByName(pagination, request.Hibeat!);
                if (consultHibeat != null)
                {
                    data.AddRange(consultHibeat);
                    return data;
                }
            }
            else if (request.Artist != string.Empty)
            {
                var consultArtist = await _userService.GetAllFilterByArtistName(pagination, request.Artist!);
                if (consultArtist != null)
                {
                    data.AddRange(consultArtist);
                    return data;
                }
            }
            else if (request.MusicalInterest != null)
            {
                var consultMusicalInterest = await _hibeatService.GetAllFilterByMusicalInterest(pagination, request.MusicalInterest!);
                if (consultMusicalInterest != null)
                {
                    data.AddRange(consultMusicalInterest);
                    return data;
                }
            }
            else
            {
                var consultHibeat = await _hibeatService.GetAllFilterByName(pagination, request.All!);
                if (consultHibeat == null)
                {
                    var consultArtist = await _userService.GetAllFilterByArtistName(pagination, request.All!);
                    if (consultArtist != null)
                    {
                        data.AddRange(consultArtist);
                        return data;
                    }
                }
                else
                {
                    data.AddRange(consultHibeat);
                    return data;
                }
            }

            return data;
        }

        public async Task<List<ReactionResponseDto>?> GetNotifications(string userId)
        {
            List<ReactionResponseDto> response = new List<ReactionResponseDto>();

            var consultUser = await _userService.GetIdByIdentifier(userId);
            if (consultUser != null)
            {
                var consultReactions = _hibeatService.GetAllNotificationByUser(consultUser.Id);
                if (consultReactions != null)
                {
                    if (consultReactions.Count > 0)
                    {
                        response.AddRange(consultReactions);
                        foreach (ReactionResponseDto item in consultReactions)
                        {
                            await _reactionService.UpdateNotification(item.Id);
                        }
                    }
                }

                var consultFollows = _userFollowerService.GetAllUserFollowNotification(consultUser.Id);
                if (consultFollows != null)
                {
                    if (consultFollows.Count > 0)
                    {
                        response.AddRange(consultFollows);
                        foreach (ReactionResponseDto item in consultFollows)
                        {
                            await _userFollowerService.UpdateNotification(item.Id);
                        }
                    }
                }
            }

            return response;
        }

        public async Task<CountNotificationDto> GetCountNotifications(string userId)
        {
            CountNotificationDto response = new CountNotificationDto();

            var consultUser = await _userService.GetIdByIdentifier(userId);
            if (consultUser != null)
            {
                var consultReactions = _hibeatService.GetAllNotificationByUser(consultUser.Id);
                if (consultReactions != null)
                {
                    if (consultReactions.Count > 0)
                    {
                        var filter = consultReactions.Where(r => r.IsRead == false).ToList();
                        response.Count += filter.Count;
                    }
                }

                var consultFollows = _userFollowerService.GetAllUserFollowNotification(consultUser.Id);
                if (consultFollows != null)
                {
                    if (consultFollows.Count > 0)
                    {
                        var filter = consultFollows.Where(r => r.IsRead == false).ToList();
                        response.Count += filter.Count;
                    }
                }
            }

            return response;
        }

        public async Task<ResponseApiDto?> SendEmailBuys(string userId)
        {
            var response = new ResponseApiDto();

            var consultUser = await _userService.GetIdByIdentifier(userId);
            if (consultUser != null)
            {
                var sendEmail = await _sendEmailRepository.SendEmailBuys(consultUser.Email);
                response.Result = sendEmail;
            }

            return response;
        }

    }
}
