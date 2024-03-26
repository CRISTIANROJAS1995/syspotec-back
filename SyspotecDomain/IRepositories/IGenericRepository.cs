using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;

namespace SyspotecDomain.IRepositories
{
    public interface IGenericRepository
    {
        Task<List<CompanyDto>?> AllCompany();
        Task<List<StateDto>?> AllState();
        Task<List<GenderDto>?> AllGender();
        Task<List<RoleDto>?> AllRole();
        Task<List<TypeIdentificationDto>?> AllTypeIdentification();
        Task<Configuration?> Configuration();
        Task<List<TypeFileDto>?> AllTypeFile();
    }
}
