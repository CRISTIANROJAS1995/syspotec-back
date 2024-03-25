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
    public interface IUserImageService
    {
        Task<ResponseApiDto?> Add(string userId, UserImageDto request);
        Task<ResponseApiDto?> Delete(string userId, UserImageDto request);
        Task<UserImage?> GetIsValid(int userId, int typeImageId);
    }
}
