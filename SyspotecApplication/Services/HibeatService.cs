using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Hibeat;
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
using SyspotecDomain.Dtos.Home;

namespace SyspotecApplication.Services
{
    public class HibeatService : IHibeatService
    {
        private readonly IHibeatRepository _hibeatRepository;
        private readonly IHiBeatInstrumentInterestRepository _hiBeatInstrumentInterestRepository;
        private readonly IHiBeatMusicalInterestRepository _hiBeatMusicalInterestRepository;
        private readonly IUserFollowerRepository _userFollowerRepository;
        private readonly IUserService _userService;

        public HibeatService(
            IHibeatRepository hibeatRepository,
            IHiBeatInstrumentInterestRepository hiBeatInstrumentInterestRepository,
            IHiBeatMusicalInterestRepository hiBeatMusicalInterestRepository,
            IUserFollowerRepository userFollowerRepository,
            IUserService userService
           )
        {
            _hibeatRepository = hibeatRepository;
            _hiBeatInstrumentInterestRepository = hiBeatInstrumentInterestRepository;
            _hiBeatMusicalInterestRepository = hiBeatMusicalInterestRepository;
            _userFollowerRepository = userFollowerRepository;
            _userService = userService;
        }

        public async Task<ResponseApiDto?> Add(string userIdentifier, HiBeatDto request)
        {
            var response = new ResponseApiDto();

            var consultUser = await _userService.GetIdByIdentifier(userIdentifier);
            if (consultUser != null)
            {
                var user = await _hibeatRepository.GetIsValid(consultUser.Id, request);
                if (user == null)
                {
                    Guid obj = Guid.NewGuid();
                    HiBeat model = new HiBeat();

                    model.Identifier = obj.ToString();
                    model.UserId = consultUser.Id;
                    model.StateId = (int)StateEnum.Active;
                    model.Title = request.Title;
                    model.Tone = request.Tone;
                    model.Duration = request.Duration;
                    model.Bpm = request.Bpm;
                    model.RecordCompany = request.RecordCompany;
                    model.UrlFile = request.UrlFile;
                    model.UrlCover = request.UrlCover;
                    model.CreatedDate = DateTime.Now;
                    model.UpdateDate = DateTime.Now;

                    var responseAdd = await _hibeatRepository.Add(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;

                        //save instruments
                        if (request.LstInstrument.Count > 0)
                        {
                            var consultHibeat = await _hibeatRepository.GetByTitle(request.Title);
                            if (consultHibeat != null)
                            {
                                foreach (InstrumentInterestDto item in request.LstInstrument)
                                {
                                    await AddHiBeatInstrumentInterest(consultHibeat.Id, item);
                                }
                            }
                        }

                        //save musical interest
                        if (request.LstMusicalInterest.Count > 0)
                        {
                            var consultHibeat = await _hibeatRepository.GetByTitle(request.Title);
                            if (consultHibeat != null)
                            {
                                foreach (MusicalInterestDto item in request.LstMusicalInterest)
                                {
                                    await AddHiBeatMusicalInterest(consultHibeat.Id, item);
                                }
                            }
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar el hibeat.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "El hibeat ya se encuentra registrado.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario no se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(string userIdentifier, HiBeatDto request)
        {
            var response = new ResponseApiDto();

            var consultUser = await _userService.GetIdByIdentifier(userIdentifier);
            if (consultUser != null)
            {
                var hibeat = await _hibeatRepository.GetIsValid(consultUser.Id, request);
                if (hibeat != null)
                {
                    hibeat.Identifier = hibeat.Identifier;
                    hibeat.UserId = hibeat.UserId;
                    hibeat.StateId = request.StateId;
                    hibeat.Title = request.Title;
                    hibeat.Tone = request.Tone;
                    hibeat.Duration = request.Duration;
                    hibeat.Bpm = request.Bpm;
                    hibeat.RecordCompany = request.RecordCompany;
                    hibeat.UrlFile = request.UrlFile;
                    hibeat.UrlCover = request.UrlCover;
                    hibeat.CreatedDate = hibeat.CreatedDate;
                    hibeat.UpdateDate = DateTime.Now;

                    var responseAdd = await _hibeatRepository.Update(hibeat);
                    if (responseAdd == 1)
                    {
                        response.Result = true;

                        //save instruments
                        if (request.LstInstrument.Count > 0)
                        {
                            await DeleteHiBeatInstrumentInterest(hibeat.Id);

                            foreach (InstrumentInterestDto item in request.LstInstrument)
                            {
                                await AddHiBeatInstrumentInterest(hibeat.Id, item);
                            }
                        }

                        //save musical interest
                        if (request.LstMusicalInterest.Count > 0)
                        {
                            await DeleteHiBeatMusicalInterest(hibeat.Id);

                            foreach (MusicalInterestDto item in request.LstMusicalInterest)
                            {
                                await AddHiBeatMusicalInterest(hibeat.Id, item);
                            }
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al actualizar el hibeat.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "El hibeat no se encuentra registrado.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario no se encuentra registrado.";
            }

            return response;
        }

        public async Task<List<HibeatResponseDto>?> GetAll(PaginationDto pagination)
        {
            var list = await _hibeatRepository.GetAll(pagination);
            return list;
        }

        public async Task<List<HibeatResponseDto>?> GetAllAdmin()
        {
            var list = await _hibeatRepository.GetAllAdmin();
            return list;
        }

        public async Task<List<HibeatResponseDto>?> GetByUserIdentifier(string identifier)
        {
            var hibeat = await _hibeatRepository.GetByUserIdentifier(identifier);
            return hibeat;
        }

        public async Task<List<HibeatResponseDto>?> GetAllFilterByName(PaginationDto pagination, string name)
        {
            var list = await _hibeatRepository.GetAllFilterByName(pagination, name);
            return list;
        }

        public async Task<List<HibeatResponseDto>?> GetAllFilterByMusicalInterest(PaginationDto pagination, MusicalInterestDto musicalInterest)
        {
            var list = await _hibeatRepository.GetAllFilterByMusicalInterest(pagination, musicalInterest);
            return list;
        }

        public async Task<HibeatResponseDto?> GetByIdentifier(string identifier)
        {
            var hibeat = _hibeatRepository.GetByIdentifier(identifier);
            return hibeat;
        }

        public async Task<HiBeat?> GetIdByIdentifier(string identifier)
        {
            var hibeat = await _hibeatRepository.GetIdByIdentifier(identifier);
            return hibeat;
        }

        public async Task<HiBeat?> GetById(int id)
        {
            var hibeat = await _hibeatRepository.GetById(id);
            return hibeat;
        }

        public async Task<List<HibeatResponseDto>?> GetTopLastWeek()
        {
            var list = await _hibeatRepository.GetTopLastWeek();
            return list;
        }

        public async Task<List<HibeatResponseDto>?> GetFeed(string identifier, PaginationDto pagination)
        {
            List<HibeatResponseDto> feedResponse = new List<HibeatResponseDto>();

            var consultUser = await _userService.GetIdByIdentifier(identifier);
            if (consultUser != null)
            {
                var consultFollower = await _userFollowerRepository.GetAllByUserPagination(consultUser.Id, pagination);
                if (consultFollower != null)
                {
                    if (consultFollower.Count > 0)
                    {
                        foreach (UserFollower follower in consultFollower)
                        {
                            var hibeat = await _hibeatRepository.GetFeed(follower.UserIdFollower);
                            if (hibeat != null)
                            {
                                feedResponse.Add(hibeat);
                            }
                        }
                    }
                }
            }

            return feedResponse;
        }

        public async Task<List<PlayListResponseDto>?> GetAllPlayList(PlayListDto pagination)
        {
            var list = await _hibeatRepository.GetAllPlayList(pagination);
            return list;
        }

        public async Task<List<HibeatResponseDto>?> GetByLike(string identifier, PaginationDto pagination)
        {
            var list = await _hibeatRepository.GetByLike(identifier, pagination);
            return list;
        }

        public async Task<List<UserResponseSummaryDto>?> GetAllByMap()
        {
            var list = await _hibeatRepository.GetAllByMap();
            return list;
        }

        public List<ReactionResponseDto> GetAllNotificationByUser(int userId)
        {
            var list = _hibeatRepository.GetAllNotificationByUser(userId);
            return list;
        }

        #region HiBeatInstrumentInterest

        private async Task<ResponseApiDto?> AddHiBeatInstrumentInterest(int hibeatId, InstrumentInterestDto request)
        {
            var response = new ResponseApiDto();

            var isValid = await GetIsValidHiBeatInstrumentInterest(hibeatId, request.Id);
            if (isValid == null)
            {
                HiBeatInstrumentInterest model = new HiBeatInstrumentInterest();
                model.HiBeatId = hibeatId;
                model.InstrumentInterestId = request.Id;
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                var responseAdd = await _hiBeatInstrumentInterestRepository.Add(model);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar el instrumento de interes del hibeat.";
                }
            }
            else
            {
                response.Result = true;
            }

            return response;
        }

        private async Task<ResponseApiDto?> DeleteHiBeatInstrumentInterest(int hibeatId)
        {
            var response = new ResponseApiDto();
            var consultUser = await GetById(hibeatId);
            if (consultUser != null)
            {
                var list = await GetAllHiBeatInstrumentInterest(hibeatId);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        var responseAdd = await _hiBeatInstrumentInterestRepository.Delete(list);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al eliminar los instrumentos de interés del hibeat.";
                        }
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "No tiene instrumentos de interes registrados.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El hibeat no se encuentra registrado.";
            }

            return response;
        }

        private async Task<HiBeatInstrumentInterest?> GetIsValidHiBeatInstrumentInterest(int hibeatId, int instrumentInterestId)
        {
            return await _hiBeatInstrumentInterestRepository.GetIsValid(hibeatId, instrumentInterestId);
        }

        private async Task<List<HiBeatInstrumentInterest>?> GetAllHiBeatInstrumentInterest(int hibeatId)
        {
            var lst = await _hiBeatInstrumentInterestRepository.GetAllByHibeatId(hibeatId);
            return lst;
        }

        #endregion

        #region HiBeatMusicalInterest

        private async Task<ResponseApiDto?> AddHiBeatMusicalInterest(int hibeatId, MusicalInterestDto request)
        {
            var response = new ResponseApiDto();

            var isValid = await GetIsValidHiBeatMusicalInterest(hibeatId, request.Id);
            if (isValid == null)
            {
                HiBeatMusicalInterest model = new HiBeatMusicalInterest();
                model.HiBeatId = hibeatId;
                model.MusicalInterestId = request.Id;
                model.CreatedDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;

                var responseAdd = await _hiBeatMusicalInterestRepository.Add(model);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al guardar los géneros de interés del hibeat.";
                }
            }
            else
            {
                response.Result = true;
            }

            return response;
        }

        private async Task<ResponseApiDto?> DeleteHiBeatMusicalInterest(int hibeatId)
        {
            var response = new ResponseApiDto();
            var consultUser = await GetById(hibeatId);
            if (consultUser != null)
            {
                var list = await GetAllHiBeatMusicalInterest(hibeatId);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        var responseAdd = await _hiBeatMusicalInterestRepository.Delete(list);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al eliminar los géneros de interés del hibeat.";
                        }
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "No tiene géneros de interes registrados.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El hibeat no se encuentra registrado.";
            }

            return response;
        }

        private async Task<HiBeatMusicalInterest?> GetIsValidHiBeatMusicalInterest(int hibeatId, int musicalInterestId)
        {
            return await _hiBeatMusicalInterestRepository.GetIsValid(hibeatId, musicalInterestId);
        }

        private async Task<List<HiBeatMusicalInterest>?> GetAllHiBeatMusicalInterest(int hibeatId)
        {
            var lst = await _hiBeatMusicalInterestRepository.GetAllByHibeatId(hibeatId);
            return lst;
        }

        #endregion

    }
}
