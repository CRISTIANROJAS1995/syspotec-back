using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using Microsoft.EntityFrameworkCore;
using SyspotecDomain.Dtos.Contract;
using SyspotecDomain.Enums;

namespace SyspotecDal.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public ContractRepository(
            ApplicationDbContext context,
            IUserRepository userRepository
            )
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task<int?> Add(Contract model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(Contract model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ContractDto>?> All()
        {
            List<ContractDto> response = new List<ContractDto>();

            var list = await _context.Contract
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Company.State")
                 .Include("State")
                 .Include("TypeFile")
                 .OrderBy(c => c.CreatedDate)
                 .ToListAsync();

            if (list.Count > 0)
            {
                response.AddRange(list.AsEnumerable().Select(g => ContractDto(g)).ToList()!);
            }

            return response;
        }

        public async Task<ContractDto> ByIdentifierDto(string identifier)
        {
            var obj = await _context.Contract
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Company.State")
                 .Include("State")
                 .Include("TypeFile")
                 .FirstOrDefaultAsync(c => c.Identifier == identifier && c.StateId == (int)StateEnum.Active);

            return ContractDto(obj);
        }

        public async Task<Contract?> ByIdentifier(string identifier)
        {
            return await _context.Contract.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier && c.StateId == (int)StateEnum.Active);
        }

        public async Task<Contract?> ByName(string name)
        {
            return await _context.Contract.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name && c.StateId == (int)StateEnum.Active);
        }

        public async Task<Company?> CompanyByIdentifier(string identifier)
        {
            return await _context.Company.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier);
        }

        #region User Contract

        public async Task<int?> AddUserContract(UserContract model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> UpdateUserContract(UserContract model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserContract?> UserContractFilter(int contractId, int userId)
        {
            return await _context.UserContract
                 .AsNoTracking()
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.ContractId == contractId && c.UserId == userId)
                 .FirstOrDefaultAsync();
        }

        public async Task<UserContractDto?> UserContractDtoFilter(int contractId, int userId)
        {
            UserContractDto response = new UserContractDto();

            var consult = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.ContractId == contractId && c.UserId == userId)
                 .FirstOrDefaultAsync();

            if (consult != null)
            {
                return response = await UserContractDto(consult);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserContractDto>?> AllUserContract()
        {
            List<UserContractDto> response = new List<UserContractDto>();

            var list = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (UserContract item in list)
                {
                    response.Add(await UserContractDto(item));
                }
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContractByContract(int contractId)
        {
            List<UserContractDto> response = new List<UserContractDto>();

            var list = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.ContractId == contractId)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (UserContract item in list)
                {
                    response.Add(await UserContractDto(item));
                }
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContractByUser(string userId)
        {
            List<UserContractDto> response = new List<UserContractDto>();
            var consultUser = await _userRepository.ByIdentifier(userId);
            if (consultUser != null)
            {
                var list = await _context.UserContract
                                .AsNoTracking()
                                .Include("Contract")
                                .Include("Contract.Company")
                                .Include("Contract.Company.State")
                                .Include("Contract.State")
                                .Include("Contract.TypeFile")
                                .Include("User")
                                .Include("State")
                                .OrderBy(c => c.CreatedDate)
                                .Where(c => c.UserId == consultUser.Id)
                                .ToListAsync();

                if (list.Count > 0)
                {
                    foreach (UserContract item in list)
                    {
                        response.Add(await UserContractDto(item));
                    }
                }
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContractByAssign(string userAssign)
        {
            List<UserContractDto> response = new List<UserContractDto>();

            var list = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.UserAssign == userAssign)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (UserContract item in list)
                {
                    response.Add(await UserContractDto(item));
                }
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContractByState(StateEnum stateId)
        {
            List<UserContractDto> response = new List<UserContractDto>();

            var list = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.StateId == (int)stateId)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (UserContract item in list)
                {
                    response.Add(await UserContractDto(item));
                }
            }

            return response;
        }

        public async Task<List<UserContractDto>?> AllUserContractByUserState(StateEnum stateId, int userId)
        {
            List<UserContractDto> response = new List<UserContractDto>();

            var list = await _context.UserContract
                 .AsNoTracking()
                 .Include("Contract")
                 .Include("Contract.Company")
                 .Include("Contract.Company.State")
                 .Include("Contract.State")
                 .Include("Contract.TypeFile")
                 .Include("User")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .Where(c => c.StateId == (int)stateId && c.UserId == userId)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (UserContract item in list)
                {
                    response.Add(await UserContractDto(item));
                }
            }

            return response;
        }

        #endregion

        private ContractDto ContractDto(Contract? consult)
        {
            ContractDto response = new ContractDto();

            if (consult != null)
            {
                StateDto objStateCompany = new StateDto();
                objStateCompany.Id = consult.Company.State.Id;
                objStateCompany.Name = consult.Company.State.Name;

                CompanyDto objCompany = new CompanyDto();
                objCompany.Identifier = consult.Company.Identifier;
                objCompany.State = objStateCompany;
                objCompany.Name = consult.Company.Name;
                objCompany.Nit = consult.Company.Nit;
                objCompany.Phone = consult.Company.Phone;
                objCompany.Address = consult.Company.Address;
                objCompany.Description = consult.Company.Description;

                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                TypeFileDto objTypeFile = new TypeFileDto();
                objTypeFile.Id = consult.TypeFile.Id;
                objTypeFile.Name = consult.TypeFile.Name;

                response.Identifier = consult.Identifier;
                response.Company = objCompany;
                response.State = objState;
                response.TypeFile = objTypeFile;
                response.Name = consult.Name;
                response.Descripcion = consult.Descripcion;
                response.Url = consult.Url;
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
            }

            return response;
        }

        private async Task<UserContractDto> UserContractDto(UserContract? consult)
        {
            UserContractDto response = new UserContractDto();

            if (consult != null)
            {
                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Contract = ContractDto(consult.Contract);
                response.State = objState;
                response.User = await _userRepository.ByIdentifierDto(consult.User.Identifier);
                response.UserAssign = await _userRepository.ByIdentifierDto(consult.UserAssign);
                response.CreatedDate = consult.CreatedDate;
                response.UpdateDate = consult.UpdateDate;
            }

            return response;
        }

    }
}
