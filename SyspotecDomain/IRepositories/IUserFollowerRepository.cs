using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IUserFollowerRepository
    {
        Task<int?> Add(UserFollower model);
        Task<int?> Update(UserFollower model);
        Task<UserFollower?> GetById(int id);
        Task<List<UserFollower>?> GetAllByIsRead();
        Task<int?> Delete(UserFollower model);
        Task<List<UserFollower>?> GetAllByUser(int userId);
        Task<UserFollower?> GetUserFollow(int userId, int userIdFollow);
        Task<List<UserFollower>?> GetAllByUserPagination(int userId, PaginationDto pagination);
        List<ReactionResponseDto> GetAllUserFollowNotification(int userIdFollow);
    }
}
