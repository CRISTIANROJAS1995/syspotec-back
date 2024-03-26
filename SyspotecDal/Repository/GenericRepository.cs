using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDal.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyDto>?> AllCompany()
        {
            List<CompanyDto> response = new List<CompanyDto>();
            var consult = await _context.Company
                                .Include("State")
                                .OrderBy(c => c.Id)
                                .ToListAsync();

            if (consult.Count > 0)
            {
                response.AddRange(consult.AsEnumerable().Select(g => CompanyDto(g)).ToList()!);
            }

            return response;
        }

        public async Task<List<StateDto>?> AllState()
        {
            return await _context.State
                                .OrderBy(c => c.Id)
                                .Select(g => new StateDto { Id = g.Id, Name = g.Name })
                                .ToListAsync();
        }

        public async Task<List<GenderDto>?> AllGender()
        {
            return await _context.Gender
                                .OrderBy(c => c.Id)
                                .Select(g => new GenderDto { Id = g.Id, Name = g.Name })
                                .ToListAsync();
        }

        public async Task<List<RoleDto>?> AllRole()
        {
            return await _context.Role
                                .OrderBy(c => c.Id)
                                .Select(g => new RoleDto { Id = g.Id, Name = g.Name })
                                .ToListAsync();
        }

        public async Task<List<TypeIdentificationDto>?> AllTypeIdentification()
        {
            return await _context.TypeIdentification
                                .OrderBy(c => c.Id)
                                .Select(g => new TypeIdentificationDto { Id = g.Id, Name = g.Name })
                                .ToListAsync();
        }

        public async Task<Configuration?> Configuration()
        {
            return await _context.Configuration
                                .OrderBy(c => c.Id)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<TypeFileDto>?> AllTypeFile()
        {
            return await _context.TypeFile
                                .OrderBy(c => c.Id)
                                .Select(g => new TypeFileDto { Id = g.Id, Name = g.Name })
                                .ToListAsync();
        }

        private CompanyDto CompanyDto(Company consult)
        {
            CompanyDto response = new CompanyDto();

            if (consult != null)
            {
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.State = objState;
                response.Name = consult.Name;
                response.Nit = consult.Nit;
                response.Phone = consult.Phone;
                response.Address = consult.Address;
                response.Description = consult.Description;
            }

            return response;
        }
    }
}
