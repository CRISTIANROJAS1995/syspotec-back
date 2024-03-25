using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IHibeatRepository
    {
        Task<List<HibeatResponseDto>?> GetAll(PaginationDto pagination);
        Task<List<HibeatResponseDto>?> GetAllAdmin();
        Task<List<HibeatResponseDto>?> GetByUserIdentifier(string identifier);
        Task<List<HibeatResponseDto>?> GetAllFilterByName(PaginationDto pagination, string name);
        Task<List<HibeatResponseDto>?> GetAllFilterByMusicalInterest(PaginationDto pagination, MusicalInterestDto musicalInterest);
        List<HibeatResponseDto> GetByUserId(int userId);
        HibeatResponseDto? GetByIdentifier(string identifier, int challengePoint = 0);
        HibeatResponseDto? GetByIdentifierSummary(string identifier, int challengePoint = 0);
        Task<HiBeat?> GetIdByIdentifier(string identifier);
        Task<HiBeat?> GetById(int id);
        Task<HiBeat?> GetByTitle(string title);
        Task<HiBeat?> GetIsValid(int userId, HiBeatDto request);
        Task<int?> Add(HiBeat model);
        Task<int?> Update(HiBeat model);
        Task<List<HibeatResponseDto>?> GetTopLastWeek();
        Task<HibeatResponseDto?> GetFeed(int userId);
        Task<List<PlayListResponseDto>?> GetAllPlayList(PlayListDto pagination);
        Task<List<HibeatResponseDto>?> GetByLike(string identifier, PaginationDto pagination);
        Task<List<UserResponseSummaryDto>?> GetAllByMap();
        HibeatResponseDto GetHibeatResponseAsync(HiBeat? consult, int challengePoint = 0);
        HibeatResponseDto GetHibeatResponseSummaryAsync(HiBeat? consult, int challengePoint = 0);
        List<ReactionResponseDto> GetReactionByHibeatIdentifier(List<Reaction> reactions);
        ReactionResponseDto GetReactionResponseAsync(Reaction? consult);
        List<ReactionResponseDto> GetAllNotificationByUser(int userId);


        }
}
