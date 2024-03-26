using SyspotecDomain.Dtos.Contract;
using SyspotecDomain.Entities;

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
    }
}
