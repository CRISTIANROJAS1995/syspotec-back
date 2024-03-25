using SyspotecDomain.Dtos.Generic;

namespace SyspotecDomain.IServices
{
    public interface IGenericService
    {
        Task<List<CompanyDto>?> AllCompany();
        Task<List<StateDto>?> AllState();
        Task<List<GenderDto>?> AllGender();
        Task<List<RoleDto>?> AllRole();
        Task<List<TypeIdentificationDto>?> AllTypeIdentification();
    }
}
