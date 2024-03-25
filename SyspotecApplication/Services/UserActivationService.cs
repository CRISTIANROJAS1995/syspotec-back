using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecApplication.Services
{
    public class UserActivationService : IUserActivationService
    {
        private readonly IUserActivationRepository _userActivationRepository;

        public UserActivationService(
           IUserActivationRepository userActivationRepository)
        {
            _userActivationRepository = userActivationRepository;
        }

        public async Task<ResponseApiDto?> AddOrUpdate(UserActivation model)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userActivationRepository.GetByEmail(model.Email);

            if (consultUser == null)
            {
                UserActivation request = new UserActivation();

                request.Email = model.Email;
                request.EmailConfirm = false;
                request.CreatedDate = DateTime.Now;
                request.UpdateDate = DateTime.Now;

                var addActivation = await _userActivationRepository.Add(request);
                if (addActivation == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar el usuario a verificar.";
                }
            }
            else
            {
                consultUser.EmailConfirm = model.EmailConfirm;
                consultUser.UpdateDate = DateTime.Now;

                var updateActivation = await _userActivationRepository.Update(consultUser);
                if (updateActivation == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar la activación del usuario.";
                }

            }

            return response;
        }

        public async Task<UserActivation?> GetByEmailActivation(string email)
        {
            var userActivation = await _userActivationRepository.GetByEmailActivation(email);
            return userActivation;
        }


    }
}
