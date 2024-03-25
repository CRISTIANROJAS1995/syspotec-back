using SyspotecApplication.Services;
using SyspotecDomain.Dtos;
//using SyspotecDomain.Dtos.User;
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

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //[Route("auth")]
        //public async Task<IActionResult> AuthenticateAsync([FromBody] RequestLoginDto request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(await _userService.AuthenticateAsync(request));
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //[Route("ValidEmail")]
        //public async Task<IActionResult> ValidEmail([FromBody] ValidEmailDto request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(await _userService.GetByEmail(request));
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //[Route("ValidUserName")]
        //public async Task<IActionResult> ValidUserName([FromBody] ValidUserNameDto request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(await _userService.GetByUserName(request));
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> Add([FromBody] UserDto request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(await _userService.Add(request));
        //}

        //[Authorize]
        //[HttpPut]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(500)]
        //public async Task<IActionResult> Update([FromBody] UserUpdateDto request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(await _userService.Update(request, User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("GetAll")]
        //[ProducesResponseType(200, Type = typeof(List<UserResponseDto>))]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> GetAll([FromQuery] PaginationDto pagination)
        //{
        //    var response = await _userService.GetAll(pagination);
        //    return Ok(response);
        //}

        //[Authorize]
        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(UserResponseDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> Get()
        //{
        //    var consult = await _userService.GetByIdentifier(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    if (consult.Identifier == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(consult);
        //}

        //[Authorize]
        //[HttpGet("{id}")]
        //[ProducesResponseType(200, Type = typeof(UserResponseDto))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    var consult = await _userService.GetByIdentifier(id);
        //    if (consult.Identifier == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(consult);
        //}


    }
}
