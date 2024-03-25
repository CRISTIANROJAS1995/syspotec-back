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
    public interface IUserBiographyService
    {
        Task<ResponseApiDto?> AddOrUpdate(UserBiography request);
    }
}
