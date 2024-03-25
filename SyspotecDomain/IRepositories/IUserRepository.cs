using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Input;

namespace SyspotecDomain.IRepositories
{
    public interface IUserRepository
    {
        Task<int?> Add(User model);
        Task<int?> Update(User model);
        Task<List<UserDto>?> All();
        Task<UserDto> ByIdentifierDto(string identifier);
        Task<User?> ByIdentifier(string identifier);
        Task<User?> ByEmail(string email);
        Task<Company?> CompanyByIdentifier(string identifier);
        Task<UserDto> ValidAuth(LoginInput model);
    }
}
