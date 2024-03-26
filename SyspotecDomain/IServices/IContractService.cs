using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Contract;
using SyspotecDomain.Entities;
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
    }
}
