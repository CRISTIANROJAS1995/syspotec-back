using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Entities;

namespace SyspotecDomain.IServices
{
    public interface IUserMapService
    {
        Task<ResponseApiDto?> Add(string userId, UserMapDto request);
        Task<ResponseApiDto?> Update(string userId, UserMapDto request);
        Task<ResponseApiDto?> Delete(string userId);
        Task<UserMap?> GetIsValid(int userId);
    }
}
