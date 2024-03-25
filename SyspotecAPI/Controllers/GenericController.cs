using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Range = SyspotecDomain.Entities.Range;

namespace SyspotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericService _genericService;
        private readonly IChallengeService _challengeService;

        public GenericController(IGenericService genericService, IChallengeService challengeService)
        {
            _genericService = genericService;
            _challengeService = challengeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAppConfiguration")]
        [ProducesResponseType(200, Type = typeof(List<AppConfiguration>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppConfiguration()
        {
            var response = await _genericService.GetAppConfiguration();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllGender")]
        [ProducesResponseType(200, Type = typeof(List<Gender>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllGender()
        {
            var response = await _genericService.GetAllGender();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllInstrumentInterest")]
        [ProducesResponseType(200, Type = typeof(List<InstrumentInterest>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllInstrumentInterest()
        {
            var response = await _genericService.GetAllInstrumentInterest();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllMusicalInterest")]
        [ProducesResponseType(200, Type = typeof(List<MusicalInterest>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllMusicalInterest()
        {
            var response = await _genericService.GetAllMusicalInterest();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllRange")]
        [ProducesResponseType(200, Type = typeof(List<Range>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllRange()
        {
            var response = await _genericService.GetAllRange();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllSocialInterest")]
        [ProducesResponseType(200, Type = typeof(List<SocialInterest>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllSocialInterest()
        {
            var response = await _genericService.GetAllSocialInterest();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllStore")]
        [ProducesResponseType(200, Type = typeof(List<Store>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllStore()
        {
            var response = await _genericService.GetAllStore();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllSubscription")]
        [ProducesResponseType(200, Type = typeof(List<SubscriptionDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllSubscription()
        {
            var response = await _genericService.GetAllSubscription();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllTypeSubscription")]
        [ProducesResponseType(200, Type = typeof(List<TypeSubscription>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllTypeSubscription()
        {
            var response = await _genericService.GetAllTypeSubscription();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllTypeImage")]
        [ProducesResponseType(200, Type = typeof(List<TypeImage>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllTypeImage()
        {
            var response = await _genericService.GetAllTypeImage();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllTypeReaction")]
        [ProducesResponseType(200, Type = typeof(List<TypeReaction>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllTypeReaction()
        {
            var response = await _genericService.GetAllTypeReaction();
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllChallenge")]
        [ProducesResponseType(200, Type = typeof(List<ChallengeDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllChallenge()
        {
            var response = await _challengeService.GetAllSummary();
            return Ok(response);
        }


        [Authorize]
        [HttpGet]
        [Route("GetAllPlayList")]
        [ProducesResponseType(200, Type = typeof(List<PlayListGenericResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllPlayList()
        {
            var response = await _genericService.GetAllPlayList();
            return Ok(response);
        }
    }
}
