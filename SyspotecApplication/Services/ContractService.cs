using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using SyspotecDomain.Enums;
using SyspotecDomain.Input;
using SyspotecDomain.Dtos.Contract;

namespace SyspotecApplication.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(
            IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<ResponseApiDto?> Add(ContractInput request)
        {
            var response = new ResponseApiDto();

            var user = await _contractRepository.ByName(request.Name);
            if (user == null)
            {
                var consultCompany = await _contractRepository.CompanyByIdentifier(request.CompanyId);
                if (consultCompany != null)
                {
                    Guid obj = Guid.NewGuid();
                    Contract model = new()
                    {
                        Identifier = obj.ToString(),
                        CompanyId = consultCompany.Id,
                        StateId = (int)StateEnum.Active,
                        TypeFileId = (int)TypeFileEnum.DigitalContract,
                        Name = request.Name,
                        Descripcion = request.Descripcion,
                        Url = request.Url,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };

                    var responseAdd = await _contractRepository.Add(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar el contrato.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "La compañía no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El contrato ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(ContractUpdateInput request)
        {
            var response = new ResponseApiDto();

            var contract = await _contractRepository.ByName(request.Name!);
            if (contract == null)
            {
                response.Result = false;
                response.Message = "El contrato no existe.";
            }
            else
            {
                contract.StateId = request.StateId > 0 ? request.StateId : contract.StateId;
                contract.Descripcion = request.Descripcion != null ? request.Descripcion : contract.Descripcion;
                contract.Url = request.Url != null ? request.Url : contract.Url;
                contract.UpdateDate = DateTime.Now;

                var responseUpdate = await _contractRepository.Update(contract);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar el contrato.";
                }
            }

            return response;
        }

        public async Task<List<ContractDto>?> All()
        {
            return await _contractRepository.All();
        }

        public async Task<ContractDto> ByIdentifierDto(string identifier)
        {
            return await _contractRepository.ByIdentifierDto(identifier);
        }

        public async Task<Contract?> ByIdentifier(string identifier)
        {
            return await _contractRepository.ByIdentifier(identifier);
        }

        public async Task<Contract?> ByName(string name)
        {
            return await _contractRepository.ByName(name);
        }

        public async Task<Company?> CompanyByIdentifier(string identifier)
        {
            return await _contractRepository.CompanyByIdentifier(identifier);
        }
    }
}
