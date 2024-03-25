using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Hibeat;

namespace SyspotecApplication.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly IUserService _userService;
        private readonly IHibeatService _hibeatService;

        public ReactionService(
            IReactionRepository reactionRepository,
            IUserService userService,
            IHibeatService hibeatService)
        {
            _reactionRepository = reactionRepository;
            _userService = userService;
            _hibeatService = hibeatService;
        }

        public async Task<ResponseApiDto?> Add(string userId, ReactionDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultHibeat = await _hibeatService.GetIdByIdentifier(request.HiBeatId);
                if (consultHibeat != null)
                {
                    Reaction modelReaction = new Reaction();

                    modelReaction.UserId = consultUser.Id;
                    modelReaction.TypeReactionId = request.TypeReactionId;
                    modelReaction.HiBeatId = consultHibeat.Id;
                    modelReaction.StateId = (int)StateEnum.Active;
                    modelReaction.Description = request.Description;
                    modelReaction.IsRead = false;
                    modelReaction.CreatedDate = DateTime.Now;
                    modelReaction.UpdateDate = DateTime.Now;

                    var isValid = await GetIsValid(consultUser.Id, request.TypeReactionId, consultHibeat.Id);
                    if (isValid == null)
                    {
                        var responseAdd = await _reactionRepository.Add(modelReaction);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al guardar la reacción del hibeat.";
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado la reacción ya fue guardada anteriormente.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el hibeat no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<ResponseApiDto?> Update(string userId, ReactionDto request)
        {
            var response = new ResponseApiDto();
            var consultUser = await _userService.GetIdByIdentifier(userId);

            if (consultUser != null)
            {
                var consultHibeat = await _hibeatService.GetIdByIdentifier(request.HiBeatId);
                if (consultHibeat != null)
                {
                    var isValid = await GetIsValid(consultUser.Id, request.TypeReactionId, consultHibeat.Id);
                    if (isValid != null)
                    {
                        isValid.StateId = request.StateId;
                        isValid.Description = request.Description;
                        isValid.IsRead = request.IsRead;
                        isValid.UpdateDate = DateTime.Now;

                        var responseAdd = await _reactionRepository.Update(isValid);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al actualizar la reacción del hibeat.";
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado no existe la reacción a actualizar";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado el hibeat no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado el usuario no existe.";
            }
            return response;
        }

        public async Task<Reaction?> GetById(int id)
        {
            return await _reactionRepository.GetById(id);
        }

        public async Task<ResponseApiDto?> UpdateNotification(int id)
        {
            var response = new ResponseApiDto();

            var consult = await GetById(id);
            if (consult != null)
            {
                consult.IsRead = true;
                consult.UpdateDate = DateTime.Now;

                var responseAdd = await _reactionRepository.Update(consult);
                if (responseAdd == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar la notificación.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "Ocurrio un error inesperado no existe la notificación a actualizar";
            }

            return response;
        }

        public async Task<Reaction?> GetIsValid(int userId, int typeReactionId, int hibeatId)
        {
            return await _reactionRepository.GetIsValid(userId, typeReactionId, hibeatId);
        }

        public async Task<List<Reaction>?> GetByUserDailyAchievement(int userId, int typeReactionId)
        {
            return await _reactionRepository.GetByUserDailyAchievement(userId, typeReactionId);
        }
    }
}
