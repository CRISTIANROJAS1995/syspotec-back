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

namespace SyspotecApplication.Services
{
    public class UserBlockService : IUserBlockService
    {
        private readonly IUserBlockRepository _userBlockRepository;
        private readonly IUserService _userService;

        public UserBlockService(
            IUserBlockRepository userBlockRepository, IUserService userService)
        {
            _userBlockRepository = userBlockRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> Add(string userId, string userBlock)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultUserBlock = await _userService.GetIdByIdentifier(userBlock);
                if (consultUserBlock != null)
                {
                    UserBlock modelUserBlock = new UserBlock();

                    modelUserBlock.UserId = consultUser.Id;
                    modelUserBlock.UserIdBlock = consultUserBlock.Id;
                    modelUserBlock.CreatedDate = DateTime.Now;
                    modelUserBlock.UpdateDate = DateTime.Now;

                    var validExits = await GetUserBlock(consultUserBlock.Id);
                    if (validExits == null)
                    {
                        var responseAdd = await _userBlockRepository.Add(modelUserBlock);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al guardar el usuario a bloquear.";
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado el usuario a bloquear ya lo esta.";

                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el usuario a bloquear no existe.";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<ResponseApiDto?> Delete(string userId, string userBlock)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultUserBlock = await _userService.GetIdByIdentifier(userBlock);
                if (consultUserBlock != null)
                {
                    var validExits = await GetUserBlock(consultUserBlock.Id);
                    if (validExits != null)
                    {
                        var responseAdd = await _userBlockRepository.Delete(validExits);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al guardar el usuario a bloquear.";
                        }
                    }

                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el usuario a desbloquear no existe.";
                }

            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario existe.";
            }
            return response;
        }

        public async Task<List<UserBlock>?> GetAllByUser(int userId)
        {
            var lst = await _userBlockRepository.GetAllByUser(userId);
            return lst;
        }

        public async Task<UserBlock?> GetUserBlock(int userIdBlock)
        {
            return await _userBlockRepository.GetUserBlock(userIdBlock);
        }

    }
}
