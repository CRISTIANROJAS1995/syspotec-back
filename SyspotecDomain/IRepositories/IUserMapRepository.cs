using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserMapRepository
    {
        Task<int?> Add(UserMap model);
        Task<int?> Update(UserMap model);
        Task<int?> Delete(UserMap model);
        Task<UserMap?> GetIsValid(int userId);
    }
}
