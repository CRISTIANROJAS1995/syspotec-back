using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.Input;

namespace SyspotecDomain.IServices
{
    public interface IUserService
    {
        Task<ResponseApiDto> Authenticate(LoginInput request);
        Task<ResponseApiDto?> Add(UserInput request);
        Task<ResponseApiDto?> Update(UserUpdateInput request, string identifier);
        Task<List<UserDto>?> All();
        Task<UserDto> ByIdentifierDto(string identifier);
        Task<User?> ByIdentifier(string identifier);
        Task<User?> ByEmail(string email);
        Task<Company?> CompanyByIdentifier(string identifier);
        Task<ResponseApiDto?> AddUserFile(UserFileInput request, string userId);
        Task<ResponseApiDto?> UpdateUserFile(UserFileUpdateInput request, string identifier);
        Task<List<UserFileDto>?> AllFileByUser(string userId);
    }
}
