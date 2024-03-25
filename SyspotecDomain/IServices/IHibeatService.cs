using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;

namespace SyspotecDomain.IServices
{
    public interface IHibeatService
    {
        Task<ResponseApiDto?> Add(string userIdentifier, HiBeatDto request);
        Task<ResponseApiDto?> Update(string userIdentifier, HiBeatDto request);
        Task<List<HibeatResponseDto>?> GetAll(PaginationDto pagination);
        Task<List<HibeatResponseDto>?> GetAllAdmin();
        Task<List<HibeatResponseDto>?> GetByUserIdentifier(string identifier);
        Task<List<HibeatResponseDto>?> GetAllFilterByName(PaginationDto pagination, string name);
        Task<List<HibeatResponseDto>?> GetAllFilterByMusicalInterest(PaginationDto pagination, MusicalInterestDto musicalInterest);
        Task<HibeatResponseDto?> GetByIdentifier(string identifier);
        Task<HiBeat?> GetIdByIdentifier(string identifier);
        Task<HiBeat?> GetById(int id);
        Task<List<HibeatResponseDto>?> GetTopLastWeek();
        Task<List<HibeatResponseDto>?> GetFeed(string identifier, PaginationDto pagination);
        Task<List<PlayListResponseDto>?> GetAllPlayList(PlayListDto pagination);
        Task<List<HibeatResponseDto>?> GetByLike(string identifier, PaginationDto pagination);
        Task<List<UserResponseSummaryDto>?> GetAllByMap();
        List<ReactionResponseDto> GetAllNotificationByUser(int userId);
    }
}
