using SyspotecDomain.Dtos;
using SyspotecDomain.Input;

namespace SyspotecDomain.IRepositories
{
    public interface IJWTManagerRepository
    {
        Task<ResponseApiDto> Authenticate(LoginInput request);
    }
}
