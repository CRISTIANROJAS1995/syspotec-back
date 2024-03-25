using SyspotecApplication.Services;
using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Challenge;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Security.Claims;

namespace SyspotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHibeatService _hibeatService;
        private readonly IUserService _userService;
        private readonly IHomeService _homeService;
        private readonly IChallengeService _challengeService;
        private readonly IUserDailyAchievementService _userDailyAchievementService;

        public HomeController(
            IHibeatService hibeatService, IUserService userService, IHomeService homeService, IChallengeService challengeService, IUserDailyAchievementService userDailyAchievementService)
        {
            _hibeatService = hibeatService;
            _userService = userService;
            _homeService = homeService;
            _challengeService = challengeService;
            _userDailyAchievementService = userDailyAchievementService;
        }


        [Authorize]
        [HttpGet]
        [Route("GetFeed")]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetFeed([FromQuery] PaginationDto pagination)
        {
            var consult = await _hibeatService.GetFeed(User.FindFirstValue(ClaimTypes.NameIdentifier), pagination);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPlayList")]
        [ProducesResponseType(200, Type = typeof(List<PlayListResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlayList([FromQuery] PlayListDto pagination)
        {
            var consult = await _hibeatService.GetAllPlayList(pagination);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetHbMyLike")]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetHbMyLikes([FromQuery] PaginationDto pagination)
        {
            var consult = await _hibeatService.GetByLike(User.FindFirstValue(ClaimTypes.NameIdentifier), pagination);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GeUserByMap")]
        [ProducesResponseType(200, Type = typeof(List<UserResponseSummaryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GeUserByMap()
        {
            var consult = await _hibeatService.GetAllByMap();
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetRanking")]
        [ProducesResponseType(200, Type = typeof(List<UserResponseSummaryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRanking()
        {
            var consult = await _userService.GetRanking();
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpPost]
        [Route("GlobalSearch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GlobalSearch([FromQuery] PaginationDto pagination, [FromBody] HiBeatFilterDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _homeService.SearchData(pagination, request));
        }

        [Authorize]
        [HttpGet]
        [Route("GetChallenge")]
        [ProducesResponseType(200, Type = typeof(List<ChallengeResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetChallenge([FromQuery] PaginationDto pagination)
        {
            var consult = await _challengeService.GetAll(pagination);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("DailyChallenge")]
        [ProducesResponseType(200, Type = typeof(List<UserDailyAchievementDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DailyChallenge()
        {
            var consult = await _userDailyAchievementService.GetDailyAchievement(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetTopArtistLastWeek")]
        [ProducesResponseType(200, Type = typeof(List<UserResponseSummaryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTopArtistLastWeek()
        {
            var consult = await _userService.GetTopLastWeek();
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }


        [Authorize]
        [HttpGet]
        [Route("GetTopHbLastWeek")]
        [ProducesResponseType(200, Type = typeof(List<UserResponseSummaryDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTopHbLastWeek()
        {
            var consult = await _hibeatService.GetTopLastWeek();
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetNotification")]
        [ProducesResponseType(200, Type = typeof(List<ReactionResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetNotification()
        {
            var consult = await _homeService.GetNotifications(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetCountNotification")]
        [ProducesResponseType(200, Type = typeof(CountNotificationDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCountNotification()
        {
            var consult = await _homeService.GetCountNotifications(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("SendEmailBuys")]
        [ProducesResponseType(200, Type = typeof(ResponseApiDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> SendEmailBuys()
        {
            var consult = await _homeService.SendEmailBuys(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
