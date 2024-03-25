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
    public class UserOtpService : IUserOtpService
    {
        private readonly IUserOtpRepository _userOtpRepository;
        private readonly ISendEmailRepository _sendEmailRepository;
        private readonly IUserActivationService _userActivationService;

        public UserOtpService(
           IUserOtpRepository userOtpRepository, ISendEmailRepository sendEmailRepository, IUserActivationService userActivationService)
        {
            _userOtpRepository = userOtpRepository;
            _sendEmailRepository = sendEmailRepository;
            _userActivationService = userActivationService;
        }

        public async Task<ResponseApiDto?> Add(SendEmailOtpDto model)
        {
            var response = new ResponseApiDto();

            Random generator = new Random();
            String generateOtp = generator.Next(0, 1000000).ToString("D6");

            var sendEmail = await SendEmailOtpAsync(model, generateOtp);

            if (sendEmail.Result)
            {
                //disabled old otps
                var consult = await _userOtpRepository.GetAllByEmail(model.Email);
                if (consult != null)
                {
                    if (consult.Count > 0)
                    {
                        foreach (UserOtp item in consult)
                        {
                            item.IsValid = false;
                            await _userOtpRepository.Update(item);
                        }
                    }
                }

                UserOtp entity = new UserOtp();
                entity.TypeOtpId = (int)model.Type == 0 ? (int)TypeOtpEnum.Register : (int)model.Type;
                entity.Email = model.Email;
                entity.Code = generateOtp;
                entity.IsValid = true;
                entity.CreatedDate = DateTime.Now;

                var responseAdd = _userOtpRepository.Add(entity);

                if (responseAdd.Result == 1)
                {
                    UserActivation requestActivation = new UserActivation();
                    requestActivation.Email = model.Email;

                    response = await _userActivationService.AddOrUpdate(requestActivation);
                }
                else
                {
                    response.Result = false;
                    response.Message = "Se envio el email pero no guardo la información";
                }
            }
            else
            {
                response = sendEmail;
            }

            return response;
        }

        public async Task<ResponseApiDto?> ValidOtp(ValidOtpDto model)
        {
            var response = new ResponseApiDto();

            var consult = await _userOtpRepository.ValidOtp(model);
            if (consult != null)
            {
                if (consult.CreatedDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    UserActivation request = new UserActivation();
                    request.Email = model.Email;
                    request.EmailConfirm = true;

                    response = await _userActivationService.AddOrUpdate(request);
                }
                else
                {
                    response.Result = false;
                    response.Message = "Código de verificación no valido.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Código de verificación no valido.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> ResendOtp(SendEmailOtpDto model)
        {
            var response = new ResponseApiDto();

            Random generator = new Random();
            String generateOtp = generator.Next(0, 1000000).ToString("D6");

            var sendEmail = await SendEmailOtpAsync(model, generateOtp);

            if (sendEmail.Result)
            {
                //disabled old otps
                var consult = await _userOtpRepository.GetAllByEmail(model.Email);
                if (consult != null)
                {
                    if (consult.Count > 0)
                    {
                        foreach (UserOtp item in consult)
                        {
                            item.IsValid = false;
                            await _userOtpRepository.Update(item);
                        }
                    }
                }

                UserOtp entity = new UserOtp();
                entity.TypeOtpId = (int)model.Type == 0 ? (int)TypeOtpEnum.Register : (int)model.Type;
                entity.Email = model.Email;
                entity.Code = generateOtp;
                entity.IsValid = true;
                entity.CreatedDate = DateTime.Now;

                var responseAdd = _userOtpRepository.Add(entity);

                if (responseAdd.Result == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Se envio el email pero no guardo la información";
                }
            }
            else
            {
                response = sendEmail;
            }

            return response;
        }

        private async Task<ResponseApiDto> SendEmailOtpAsync(SendEmailOtpDto model, string code)
        {
            var response = new ResponseApiDto();
            var sendEmail = await _sendEmailRepository.SendEmailOtpAsync(model.Email, code, model.Type.ToString());

            response.Message = sendEmail ? "Se envio correctamente el código de verificación." : "No se envio el código de verificación.";
            response.Result = sendEmail;


            return response;
        }

    }
}
