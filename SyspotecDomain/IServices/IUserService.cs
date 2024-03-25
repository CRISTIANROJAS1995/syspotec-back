using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;

namespace SyspotecDomain.IServices
{
    public interface IUserService
    {
        Task<ResponseApiDto> AuthenticateAsync(RequestLoginDto request);
        Task<ResponseApiDto?> Add(UserDto request);
        Task<ResponseApiDto?> Update(UserUpdateDto request, string identifier);
        Task<ResponseApiDto?> GetByEmail(ValidEmailDto request);
        Task<ResponseApiDto?> GetByUserName(ValidUserNameDto request);
        Task<List<UserResponseDto>?> GetAll(PaginationDto pagination);
        Task<UserResponseDto> GetByIdentifier(string identifier);
        Task<List<UserResponseDto>?> GetAllFilterByArtistName(PaginationDto pagination, string name);
        Task<User?> GetIdByIdentifier(string identifier);
        Task<List<UserResponseSummaryDto>?> GetTopLastWeek();
        Task<List<UserResponseSummaryDto>?> GetRanking();
        Task<ResponseApiDto> AuthenticateForgotAsync(RequestLoginDto request);
    }
}
