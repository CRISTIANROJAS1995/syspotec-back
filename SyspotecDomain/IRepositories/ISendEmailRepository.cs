using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface ISendEmailRepository
    {
        Task<bool> SendEmailOtpAsync(string email, string code, string type);
        bool SendEmailRecovery(string email, string guid);
        Task<bool> SendEmailBuys(string email);
    }
}
