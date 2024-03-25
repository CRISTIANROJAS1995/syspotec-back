using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IUserOtpService
    {
        Task<ResponseApiDto?> Add(SendEmailOtpDto model);
        Task<ResponseApiDto?> ValidOtp(ValidOtpDto model);
        Task<ResponseApiDto?> ResendOtp(SendEmailOtpDto model);
    }
}
