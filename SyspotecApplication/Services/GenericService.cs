using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;

namespace SyspotecApplication.Services
{
    public class GenericService : IGenericService
    {
        private readonly IGenericRepository _genericRepository;

        public GenericService(
            IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<List<CompanyDto>?> AllCompany()
        {
            return await _genericRepository.AllCompany();
        }

        public async Task<List<StateDto>?> AllState()
        {
            return await _genericRepository.AllState();
        }

        public async Task<List<GenderDto>?> AllGender()
        {
            return await _genericRepository.AllGender();
        }

        public async Task<List<RoleDto>?> AllRole()
        {
            return await _genericRepository.AllRole();
        }

        public async Task<List<TypeIdentificationDto>?> AllTypeIdentification()
        {
            return await _genericRepository.AllTypeIdentification();
        }

        public async Task<Configuration?> Configuration()
        {
            return await _genericRepository.Configuration();
        }

        public async Task<List<TypeFileDto>?> AllTypeFile()
        {
            return await _genericRepository.AllTypeFile();
        }

    }
}
