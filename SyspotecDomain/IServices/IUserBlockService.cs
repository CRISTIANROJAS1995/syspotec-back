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
    public interface IUserBlockService
    {
        Task<ResponseApiDto?> Add(string userId, string userBlock);
        Task<ResponseApiDto?> Delete(string userId, string userBlock);
        Task<List<UserBlock>?> GetAllByUser(int userId);
        Task<UserBlock?> GetUserBlock(int userIdBlock);
    }
}
