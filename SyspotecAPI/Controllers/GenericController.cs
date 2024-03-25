using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SyspotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {
        private readonly IGenericService _genericService;

        public GenericController(IGenericService genericService)
        {
            _genericService = genericService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllCompany")]
        [ProducesResponseType(200, Type = typeof(List<CompanyDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllCompany()
        {
            var response = await _genericService.AllCompany();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllState")]
        [ProducesResponseType(200, Type = typeof(List<StateDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllState()
        {
            var response = await _genericService.AllState();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllGender")]
        [ProducesResponseType(200, Type = typeof(List<GenderDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllGender()
        {
            var response = await _genericService.AllGender();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllRole")]
        [ProducesResponseType(200, Type = typeof(List<RoleDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllRole()
        {
            var response = await _genericService.AllRole();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllTypeIdentification")]
        [ProducesResponseType(200, Type = typeof(List<TypeIdentificationDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AllTypeIdentification()
        {
            var response = await _genericService.AllTypeIdentification();
            return Ok(response);
        }

    }
}
