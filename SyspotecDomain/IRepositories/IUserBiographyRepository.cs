using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserBiographyRepository
    {
        Task<int?> Add(UserBiography model);
        Task<int?> Update(UserBiography model);
        Task<UserBiography?> GetByUserId(int userId);
    }
}
