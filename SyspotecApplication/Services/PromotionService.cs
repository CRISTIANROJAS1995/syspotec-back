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
using SyspotecDomain.Dtos.Generic;

namespace SyspotecApplication.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<ResponseApiDto?> Add(Promotion request)
        {
            var response = new ResponseApiDto();
            Promotion model = new Promotion();

            model.TitleEnglish = request.TitleEnglish;
            model.TitleSpanish = request.TitleSpanish;
            model.Text1English = request.Text1English;
            model.Text1Spanish = request.Text1Spanish;
            model.Text2English = request.Text2English;
            model.Text2Spanish = request.Text2Spanish;
            model.SubtitleEnglish = request.SubtitleEnglish;
            model.SubtitleSpanish = request.SubtitleSpanish;
            model.UrlOutstandingImage = request.UrlOutstandingImage;
            model.UrlSecondaryImage = request.UrlSecondaryImage;
            model.CreatedDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;

            var responseAdd = await _promotionRepository.Add(model);
            if (responseAdd == 1)
            {
                response.Result = true;
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado al guardar la promoción.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(Promotion request)
        {
            var response = new ResponseApiDto();

            var consult = await _promotionRepository.GetById(request.Id);
            if (consult == null)
            {
                response.Result = false;
                response.Message = "La promoción no existe.";
            }
            else
            {
                consult.TitleEnglish = request.TitleEnglish;
                consult.TitleSpanish = request.TitleSpanish;
                consult.Text1English = request.Text1English;
                consult.Text1Spanish = request.Text1Spanish;
                consult.Text2English = request.Text2English;
                consult.Text2Spanish = request.Text2Spanish;
                consult.SubtitleEnglish = request.SubtitleEnglish;
                consult.SubtitleSpanish = request.SubtitleSpanish;  
                consult.UrlOutstandingImage = request.UrlOutstandingImage;
                consult.UrlSecondaryImage = request.UrlSecondaryImage;
                consult.UpdateDate = DateTime.Now;

                var responseUpdate = await _promotionRepository.Update(consult);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar la promoción.";
                }
            }

            return response;
        }

        public async Task<ResponseApiDto?> Delete(Promotion request)
        {
            var response = new ResponseApiDto();

            var consult = await _promotionRepository.GetById(request.Id);
            if (consult == null)
            {
                response.Result = false;
                response.Message = "La promoción no existe.";
            }
            else
            {
                var responseUpdate = await _promotionRepository.Delete(consult);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al eliminar la promoción.";
                }
            }

            return response;
        }

        public async Task<List<Promotion>?> GetAll()
        {
            return await _promotionRepository.GetAll();
        }
    }
}
