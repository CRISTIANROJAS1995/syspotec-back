using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserOtpRepository
    {
        Task<int?> Add(UserOtp model);
        Task<int?> Update(UserOtp model);
        Task<List<UserOtp>?> GetAllByEmail(string email);
        Task<UserOtp?> ValidOtp(ValidOtpDto model);

    }
}
