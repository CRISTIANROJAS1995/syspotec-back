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
    public class UserMapService : IUserMapService
    {
        private readonly IUserMapRepository _userMapRepository;
        private readonly IUserService _userService;

        public UserMapService(IUserMapRepository userMapRepository, IUserService userService)
        {
            _userMapRepository = userMapRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> Add(string userId, UserMapDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                UserMap modelUserMap = new UserMap();

                modelUserMap.UserId = consultUser.Id;
                modelUserMap.StateId = (int)StateEnum.Active;
                modelUserMap.Latitude = request.Latitude;
                modelUserMap.Longitude = request.Longitude;
                modelUserMap.CreatedDate = DateTime.Now;
                modelUserMap.UpdateDate = DateTime.Now;
                modelUserMap.Location = request.Location;

                var isValid = await GetIsValid(consultUser.Id);
                if (isValid == null)
                {
                    var responseAdd = await _userMapRepository.Add(modelUserMap);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar la ubicación del mapa del usuario.";
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

        public async Task<ResponseApiDto?> Update(string userId, UserMapDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consult = await GetIsValid(consultUser.Id);
                if (consult != null)
                {
                    consult.Latitude = request.Latitude;
                    consult.Longitude = request.Longitude;
                    consult.UpdateDate = DateTime.Now;
                    consult.Location = request.Location;

                    var responseAdd = await _userMapRepository.Update(consult);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al actualizar la ubicación del mapa del usuario.";
                    }
                }
                else
                {
                    response.Result = false;
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<ResponseApiDto?> Delete(string userId)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var isValid = await GetIsValid(consultUser.Id);
                if (isValid == null)
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado la ubicación ya esta inactiva.";
                }
                else
                {
                    isValid.StateId = (int)StateEnum.Inactive;
                    isValid.UpdateDate = DateTime.UtcNow;

                    var responseAdd = await _userMapRepository.Delete(isValid);
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

        public async Task<UserMap?> GetIsValid(int userId)
        {
            return await _userMapRepository.GetIsValid(userId);
        }

    }
}
