using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System.Text;
using System.Security.Cryptography;
using SyspotecDomain.Enums;
using SyspotecDomain.Dtos.User;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using SyspotecDomain.Dtos.Hibeat;

namespace SyspotecApplication.Services
{
    public class UserFollowerService : IUserFollowerService
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserService _userService;

        public UserFollowerService(IUserFollowerRepository userFollowerRepository, IUserService userService)
        {
            _userFollowerRepository = userFollowerRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> Add(string userId, string userFollow)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultUserFollower = await _userService.GetIdByIdentifier(userFollow);
                if (consultUserFollower != null)
                {
                    UserFollower modelUserFollower = new UserFollower();

                    modelUserFollower.UserId = consultUser.Id;
                    modelUserFollower.UserIdFollower = consultUserFollower.Id;
                    modelUserFollower.CreatedDate = DateTime.Now;
                    modelUserFollower.UpdateDate = DateTime.Now;
                    modelUserFollower.IsRead = false;

                    var validExits = await GetUserFollow(consultUser.Id, consultUserFollower.Id);
                    if (validExits == null)
                    {
                        var responseAdd = await _userFollowerRepository.Add(modelUserFollower);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al guardar el usuario a seguir.";
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado ya sigues a este usuario.";

                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el usuario a seguir no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<UserFollower?> GetById(int id)
        {
            return await _userFollowerRepository.GetById(id);
        }

        public async Task<List<UserFollower>?> GetAllByIsRead()
        {
            return await _userFollowerRepository.GetAllByIsRead();
        }

        public async Task<ResponseApiDto?> UpdateNotification(int id)
        {
            var response = new ResponseApiDto();

            var consult = await GetById(id);
            if (consult != null)
            {
                consult.IsRead = true;
                consult.UpdateDate = DateTime.Now;

                var responseAdd = await _userFollowerRepository.Update(consult);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar la notificación de follow.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado no existe la notificación a actualizar follow.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Delete(string userId, string userFollow)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultUserFollower = await _userService.GetIdByIdentifier(userFollow);
                if (consultUserFollower != null)
                {
                    var validExits = await GetUserFollow(consultUser.Id, consultUserFollower.Id);
                    if (validExits != null)
                    {
                        var responseAdd = await _userFollowerRepository.Delete(validExits);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al guardar el usuario a seguir.";
                        }
                    }

                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el usuario a dejar de seguir no existe.";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario existe.";
            }
            return response;
        }

        public async Task<List<UserFollower>?> GetAllByUser(int userId)
        {
            var lst = await _userFollowerRepository.GetAllByUser(userId);
            return lst;
        }

        public async Task<UserFollower?> GetUserFollow(int userId, int userIdFollow)
        {
            return await _userFollowerRepository.GetUserFollow(userId, userIdFollow);
        }

        public List<ReactionResponseDto> GetAllUserFollowNotification(int userIdFollow)
        {
            return _userFollowerRepository.GetAllUserFollowNotification(userIdFollow);
        }
    }
}
