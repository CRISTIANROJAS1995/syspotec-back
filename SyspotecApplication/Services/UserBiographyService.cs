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
    public class UserBiographyService : IUserBiographyService
    {
        private readonly IUserBiographyRepository _userBiographyRepository;

        public UserBiographyService(
            IUserBiographyRepository userBiographyRepository)
        {
            _userBiographyRepository = userBiographyRepository;
        }

        public async Task<ResponseApiDto?> AddOrUpdate(UserBiography request)
        {
            var response = new ResponseApiDto();

            var consult = await _userBiographyRepository.GetByUserId(request.UserId);
            if (consult == null)
            {
                request.CreatedDate = DateTime.Now;
                request.UpdateDate = DateTime.Now;

                var responseAdd = await _userBiographyRepository.Add(request);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar la biografía del usuario.";
                }
            }
            else
            {
                consult.Description = request.Description;
                consult.UrlFacebook = request.UrlFacebook;
                consult.UrlInstagram = request.UrlInstagram;
                consult.UrlSoundCloud = request.UrlSoundCloud;
                consult.UrlSpotify = request.UrlSpotify;
                consult.UrlWeb = request.UrlWeb;
                consult.UrlYoutube = request.UrlYoutube;

                consult.UpdateDate = DateTime.Now;

                var responseAdd = await _userBiographyRepository.Update(consult);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar la biografía del usuario.";
                }
            }

            return response;
        }
    }
}
