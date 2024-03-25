using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Entities;
using SyspotecDomain.Dtos.Hibeat;

namespace SyspotecDomain.IServices
{
    public interface IUserFollowerService
    {
        Task<ResponseApiDto?> Add(string userId, string userFollow);
        Task<UserFollower?> GetById(int id);
        Task<List<UserFollower>?> GetAllByIsRead();
        Task<ResponseApiDto?> UpdateNotification(int id);
        Task<ResponseApiDto?> Delete(string userId, string userFollow);
        Task<List<UserFollower>?> GetAllByUser(int userId);
        Task<UserFollower?> GetUserFollow(int userId, int userIdFollow);
        List<ReactionResponseDto> GetAllUserFollowNotification(int userIdFollow);
    }
}
