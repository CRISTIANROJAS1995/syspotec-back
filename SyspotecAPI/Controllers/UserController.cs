using SyspotecApplication.Services;
using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace SyspotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserOtpService _userOtpService;
        private readonly IUserBlockService _userBlockService;
        private readonly IUserFollowerService _userFollowerService;
        private readonly IUserImageService _userImageService;
        private readonly IUserMapService _userMapService;

        public UserController(
            IUserService userService,
            IUserOtpService userOtpService,
            IUserBlockService userBlockService,
            IUserFollowerService userFollowerService,
            IUserImageService userImageService,
            IUserMapService userMapService)
        {
            _userService = userService;
            _userOtpService = userOtpService;
            _userBlockService = userBlockService;
            _userFollowerService = userFollowerService;
            _userImageService = userImageService;
            _userMapService = userMapService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("auth")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RequestLoginDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.AuthenticateAsync(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("ValidEmail")]
        public async Task<IActionResult> ValidEmail([FromBody] ValidEmailDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.GetByEmail(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("ValidUserName")]
        public async Task<IActionResult> ValidUserName([FromBody] ValidUserNameDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.GetByUserName(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] UserDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Add(request));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Update(request, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<UserResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDto pagination)
        {
            var response = await _userService.GetAll(pagination);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var consult = await _userService.GetByIdentifier(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult.Identifier == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var consult = await _userService.GetByIdentifier(id);
            if (consult.Identifier == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpPost]
        [Route("AddBlock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddBlock([FromBody] RequestUserBlockDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userBlockService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request.UserBlock));
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteBlock")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteBlock([FromBody] RequestUserBlockDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userBlockService.Delete(User.FindFirstValue(ClaimTypes.NameIdentifier), request.UserBlock));
        }

        [Authorize]
        [HttpPost]
        [Route("AddFollow")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddFollow([FromBody] RequestUserFollowDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userFollowerService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request.UserFollow));
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteFollow")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteFollow([FromBody] RequestUserFollowDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userFollowerService.Delete(User.FindFirstValue(ClaimTypes.NameIdentifier), request.UserFollow));
        }

        [Authorize]
        [HttpPost]
        [Route("AddImage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddImage([FromBody] UserImageDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userImageService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteImage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteImage([FromBody] UserImageDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userImageService.Delete(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpPost]
        [Route("AddMap")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddMap([FromBody] UserMapDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userMapService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateMap")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateMap([FromBody] UserMapDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userMapService.Update(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteMap")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteMap()
        {
            return Ok(await _userMapService.Delete(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        #region Validation Activation

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("SendEmailOtp")]
        public async Task<IActionResult> SendEmailOtp([FromBody] SendEmailOtpDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userOtpService.Add(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("ValidOtp")]
        public async Task<IActionResult> ValidOtp([FromBody] ValidOtpDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userOtpService.ValidOtp(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("ResendSendEmailOtp")]
        public async Task<IActionResult> ResendSendEmailOtp([FromBody] SendEmailOtpDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userOtpService.ResendOtp(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("ForgotPwdEmailOtp")]
        public async Task<IActionResult> ForgotPwdEmailOtp([FromBody] RequestLoginDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validEmail = new ResponseApiDto();

            validEmail = await _userService.AuthenticateForgotAsync(request);

            if (validEmail.Result == true)
            {
                var response = new ResponseApiDto();
                var responsenew = new SendEmailOtpDto();
                responsenew.Email = request.Email;
                responsenew.Type = SyspotecDomain.Enums.TypeOtpEnum.Recovery;
                response = await _userOtpService.ResendOtp(responsenew);

                if (response.Result == false)
                {
                    return BadRequest(ModelState);
                }
            }

            return Ok(validEmail);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UpdateUserPwd")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateUserPwd([FromBody] UserUpdateDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Set migration 
            request.IsMigration = false;

            return Ok(await _userService.Update(request, request.identifier));
        }
        #endregion

    }
}
