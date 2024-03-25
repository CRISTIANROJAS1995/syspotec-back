using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserActivationRepository
    {
        Task<int?> Add(UserActivation model);
        Task<int?> Update(UserActivation model);
        Task<UserActivation?> GetByEmail(string email);
        Task<UserActivation?> GetByEmailActivation(string email);
    }
}
