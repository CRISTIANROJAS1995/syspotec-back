using SyspotecApplication.Services;
using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Hibeat;
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
    public class HibeatController : ControllerBase
    {
        private readonly IHibeatService _hibeatService;
        private readonly IReactionService _reactionService;

        public HibeatController(
            IHibeatService hibeatService, IReactionService reactionService)
        {
            _hibeatService = hibeatService;
            _reactionService = reactionService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] HiBeatDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _hibeatService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] HiBeatDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _hibeatService.Update(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var consult = await _hibeatService.GetByUserIdentifier(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDto pagination)
        {
            var response = await _hibeatService.GetAll(pagination);
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAdmin")]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllAdmin()
        {
            var response = await _hibeatService.GetAllAdmin();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(HibeatResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByIdentifier(string id)
        {
            var consult = await _hibeatService.GetByIdentifier(id);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpGet("User/{id}")]
        [ProducesResponseType(200, Type = typeof(List<HibeatResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByUserIdentifier(string id)
        {
            var consult = await _hibeatService.GetByUserIdentifier(id);
            if (consult == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

        [Authorize]
        [HttpPost]
        [Route("AddReaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddReaction([FromBody] ReactionDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _reactionService.Add(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateReaction")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateReaction([FromBody] ReactionDto request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _reactionService.Update(User.FindFirstValue(ClaimTypes.NameIdentifier), request));
        }

    }
}
