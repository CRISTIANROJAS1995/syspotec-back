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
    public class UserImageService : IUserImageService
    {
        private readonly IUserImageRepository _userImageRepository;
        private readonly IUserService _userService;

        public UserImageService(IUserImageRepository userImageRepository, IUserService userService)
        {
            _userImageRepository = userImageRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> Add(string userId, UserImageDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                UserImage modelUserImage = new UserImage();

                modelUserImage.UserId = consultUser.Id;
                modelUserImage.StateId = (int)StateEnum.Active;
                modelUserImage.TypeImageId = request.TypeImage.Id;
                modelUserImage.Url = request.Url;
                modelUserImage.CreatedDate = DateTime.Now;
                modelUserImage.UpdateDate = DateTime.Now;

                var isValid = await GetIsValid(consultUser.Id, request.TypeImage.Id);
                if (isValid == null)
                {
                    var responseAdd = await _userImageRepository.Add(modelUserImage);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar la imagen del usuario.";
                    }
                }
                else
                {
                    response.Result = true;
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<ResponseApiDto?> Delete(string userId, UserImageDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var isValid = await GetIsValid(consultUser.Id, request.TypeImage.Id);
                if (isValid == null)
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado la imagen ya esta inactiva.";
                }
                else
                {
                    isValid.StateId = (int)StateEnum.Inactive;
                    isValid.UpdateDate = DateTime.UtcNow;

                    var responseAdd = await _userImageRepository.Delete(isValid);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar la imagen del usuario.";
                    }
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<UserImage?> GetIsValid(int userId, int typeImageId)
        {
            return await _userImageRepository.GetIsValid(userId, typeImageId);
        }
    }
}
