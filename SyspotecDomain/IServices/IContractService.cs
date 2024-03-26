using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Contract;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;
using SyspotecDomain.Input;

namespace SyspotecDomain.IServices
{
    public interface IContractService
    {
        Task<ResponseApiDto?> Add(ContractInput request);
        Task<ResponseApiDto?> Update(ContractUpdateInput request);
        Task<List<ContractDto>?> All();
        Task<ContractDto> ByIdentifierDto(string identifier);
        Task<Contract?> ByIdentifier(string identifier);
        Task<Contract?> ByName(string name);
        Task<Company?> CompanyByIdentifier(string identifier);
        Task<ResponseApiDto?> AddUserContract(UserContractInput request);
        Task<ResponseApiDto?> UpdateUserContract(UserContractUpdateInput request, string userId);
        Task<List<UserContractDto>?> AllUserContract();
        Task<List<UserContractDto>?> AllUserContractByContract(int contractId);
        Task<List<UserContractDto>?> AllUserContractByUser(string userId);
        Task<List<UserContractDto>?> AllUserContractByAssign(string userAssign);
        Task<List<UserContractDto>?> AllUserContractByState(StateEnum stateId);
        Task<List<UserContractDto>?> AllUserContractByUserState(StateEnum stateId, int userId);
    }
}
