﻿using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using SyspotecDomain.Input;
using SyspotecDomain.Dtos.User;

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

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Route("Auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.Authenticate(request));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] UserInput request)
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
        public async Task<IActionResult> Update([FromBody] UserUpdateInput request)
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(200, Type = typeof(List<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.All();
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var consult = await _userService.ByIdentifierDto(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult.Identifier == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var consult = await _userService.ByIdentifierDto(id);
            if (consult.Identifier == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
