using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserBlockRepository
    {
        Task<int?> Add(UserBlock model);
        Task<int?> Delete(UserBlock model);
        Task<List<UserBlock>?> GetAllByUser(int userId);
        Task<UserBlock?> GetUserBlock(int userIdBlock);
    }
}
