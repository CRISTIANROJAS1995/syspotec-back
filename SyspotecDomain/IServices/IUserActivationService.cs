using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IUserActivationService
    {
        Task<ResponseApiDto?> AddOrUpdate(UserActivation model);
        Task<UserActivation?> GetByEmailActivation(string email);
    }
}
