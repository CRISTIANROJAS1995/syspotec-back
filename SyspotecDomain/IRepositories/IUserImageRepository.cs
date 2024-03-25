using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserImageRepository
    {
        Task<int?> Add(UserImage model);
        Task<int?> Delete(UserImage model);
        Task<UserImage?> GetIsValid(int userId, int typeImageId);
    }
}
