using SyspotecDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using SyspotecDomain.Input;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.IRepositories;
using SyspotecDomain.Dtos.Contract;

namespace SyspotecAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(
            IContractService contractService)
        {
            _contractService = contractService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add([FromBody] ContractInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _contractService.Add(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] ContractUpdateInput request)
        {
            if (request == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _contractService.Update(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(200, Type = typeof(List<ContractDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> All()
        {
            var response = await _contractService.All();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ContractDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(string id)
        {
            var consult = await _contractService.ByIdentifierDto(id);
            if (consult.Identifier == null)
            {
                return NotFound();
            }

            return Ok(consult);
        }

    }
}
