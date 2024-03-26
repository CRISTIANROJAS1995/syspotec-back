using SyspotecDomain.Dtos.Contract;
using SyspotecDomain.Entities;
using SyspotecDomain.Enums;

namespace SyspotecDomain.IRepositories
{
    public interface IContractRepository
    {
        Task<int?> Add(Contract model);
        Task<int?> Update(Contract model);
        Task<List<ContractDto>?> All();
        Task<ContractDto> ByIdentifierDto(string identifier);
        Task<Contract?> ByIdentifier(string identifier);
        Task<Contract?> ByName(string name);
        Task<Company?> CompanyByIdentifier(string identifier);
        Task<int?> AddUserContract(UserContract model);
        Task<int?> UpdateUserContract(UserContract model);
        Task<UserContract?> UserContractFilter(int contractId, int userId);
        Task<UserContractDto?> UserContractDtoFilter(int contractId, int userId);
        Task<List<UserContractDto>?> AllUserContract();
        Task<List<UserContractDto>?> AllUserContractByContract(int contractId);
        Task<List<UserContractDto>?> AllUserContractByUser(string userId);
        Task<List<UserContractDto>?> AllUserContractByAssign(string userAssign);
        Task<List<UserContractDto>?> AllUserContractByState(StateEnum stateId);
        Task<List<UserContractDto>?> AllUserContractByUserState(StateEnum stateId, int userId);
    }
}
