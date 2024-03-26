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
        private readonly IUserService _userService;

        public ContractService(
            IContractRepository contractRepository, IUserService userService)
        {
            _contractRepository = contractRepository;
            _userService = userService;
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

        public async Task<ResponseApiDto?> AddUserContract(UserContractInput request)
        {
            var response = new ResponseApiDto();

            var consultContract = await _contractRepository.ByIdentifier(request.ContractId);
            if (consultContract != null)
            {
                var consultUser = await _userService.ByIdentifier(request.UserId);
                if (consultUser != null)
                {
                    var consultUserContract = await _contractRepository.UserContractFilter(consultContract.Id, consultUser.Id);
                    if (consultUserContract == null)
                    {
                        UserContract model = new()
                        {
                            ContractId = consultContract.Id,
                            UserId = consultUser.Id,
                            StateId = (int)StateEnum.Assign,
                            UserAssign = request.UserAssign,
                            CreatedDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        };

                        var responseAdd = await _contractRepository.AddUserContract(model);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al asignar el contrato.";
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "El contrato ya fue asignado anteriormente a este usuario.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El contrato no existe.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> UpdateUserContract(UserContractUpdateInput request, string userId)
        {
            var response = new ResponseApiDto();

            var consultContract = await _contractRepository.ByIdentifier(request.ContractId);
            if (consultContract != null)
            {
                var consultUser = await _userService.ByIdentifier(userId);
                if (consultUser != null)
                {
                    var consultUserContract = await _contractRepository.UserContractFilter(consultContract.Id, consultUser.Id);
                    if (consultUserContract != null)
                    {
                        consultUserContract.UpdateDate = DateTime.Now;
                        consultUserContract.StateId = request.StateId;

                        var responseAdd = await _contractRepository.UpdateUserContract(consultUserContract);
                        if (responseAdd == 1)
                        {
                            response.Result = true;
                        }
                        else
                        {
                            response.Result = false;
                            response.Message = "Ocurrio un error inesperado al actualizar la asignación del contrato.";
                        }
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El contrato no existe.";
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContract()
        {
            return await _contractRepository.AllUserContract();
        }

        public async Task<List<UserContractDto>?> AllUserContractByContract(int contractId)
        {
            return await _contractRepository.AllUserContractByContract(contractId);
        }

        public async Task<List<UserContractDto>?> AllUserContractByUser(string userId)
        {
            return await _contractRepository.AllUserContractByUser(userId);
        }

        public async Task<List<UserContractDto>?> AllUserContractByAssign(string userAssign)
        {
            return await _contractRepository.AllUserContractByAssign(userAssign);
        }

        public async Task<List<UserContractDto>?> AllUserContractByState(StateEnum stateId)
        {
            return await _contractRepository.AllUserContractByState(stateId);
        }

        public async Task<List<UserContractDto>?> AllUserContractByUserState(StateEnum stateId, int userId)
        {
            return await _contractRepository.AllUserContractByUserState(stateId, userId);
        }

    }
}
