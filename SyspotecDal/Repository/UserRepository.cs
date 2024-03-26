using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using System.Linq;
using System.Web;
using SyspotecDomain.Dtos.User;
using System;
using SyspotecDomain.Enums;
using SyspotecDomain.Input;

namespace SyspotecDal.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int?> Add(User model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(User model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>?> All()
        {
            List<UserDto> response = new List<UserDto>();

            var list = await _context.User
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Company.State")
                 .Include("Role")
                 .Include("Gender")
                 .Include("TypeIdentification")
                 .Include("State")
                 .OrderBy(c => c.CreatedDate)
                 .ToListAsync();

            if (list.Count > 0)
            {
                foreach (User item in list)
                {
                    response.Add(await UserDto(item));
                }
            }

            return response;
        }

        public async Task<UserDto> ByIdentifierDto(string identifier)
        {
            var obj = await _context.User
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Company.State")
                 .Include("Role")
                 .Include("Gender")
                 .Include("TypeIdentification")
                 .Include("State")
                 .FirstOrDefaultAsync(c => c.Identifier == identifier);

            return await UserDto(obj);
        }

        public async Task<User?> ByIdentifier(string identifier)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier);
        }

        public async Task<User?> ByEmail(string email)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Company?> CompanyByIdentifier(string identifier)
        {
            return await _context.Company.AsNoTracking().FirstOrDefaultAsync(c => c.Identifier == identifier);
        }

        public async Task<UserDto> ValidAuth(LoginInput model)
        {
            var consult = await _context.User
                 .AsNoTracking()
                 .Include("Company")
                 .Include("Company.State")
                 .Include("Role")
                 .Include("Gender")
                 .Include("TypeIdentification")
                 .Include("State")
                 .FirstOrDefaultAsync((u => u.Email == model.Email && u.Password == model.Password));

            return await UserDto(consult);
        }

        #region UserFile

        public async Task<int?> AddUserFile(UserFile model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> UpdateUserFile(UserFile model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserFile?> UserFileByType(int typeFileId)
        {
            return await _context.UserFile.AsNoTracking().FirstOrDefaultAsync(c => c.TypeFileId == typeFileId);
        }

        public async Task<List<UserFileDto>?> AllFileByUser(string userId)
        {
            List<UserFileDto> response = new List<UserFileDto>();

            var consultUser = await ByIdentifier(userId);
            if (consultUser != null)
            {
                var list = await _context.UserFile
                               .AsNoTracking()
                               .Include("TypeFile")
                               .OrderBy(c => c.CreatedDate)
                               .Where(c => c.UserId == consultUser.Id)
                               .ToListAsync();

                if (list.Count > 0)
                {
                    response.AddRange(list.AsEnumerable().Select(g => UserFileDto(g)).ToList()!);
                }
            }

            return response;
        }

        private UserFileDto UserFileDto(UserFile? consult)
        {
            UserFileDto response = new UserFileDto();

            if (consult != null)
            {
                TypeFileDto objTypeFile = new TypeFileDto();
                objTypeFile.Id = consult.TypeFile.Id;
                objTypeFile.Name = consult.TypeFile.Name;

                response.TypeFile = objTypeFile;
                response.Url = consult.Url;
            }

            return response;
        }

        #endregion

        private async Task<UserDto> UserDto(User? consult)
        {
            UserDto response = new UserDto();

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

                RoleDto objRole = new RoleDto();
                objRole.Id = consult.Role.Id;
                objRole.Name = consult.Role.Name;

                GenderDto objGender = new GenderDto();
                objGender.Id = consult.Gender.Id;
                objGender.Name = consult.Gender.Name;

                TypeIdentificationDto objTypeIdentification = new TypeIdentificationDto();
                objTypeIdentification.Id = consult.TypeIdentification.Id;
                objTypeIdentification.Name = consult.TypeIdentification.Name;

                StateDto objState = new StateDto();
                objState.Id = consult.State.Id;
                objState.Name = consult.State.Name;

                response.Identifier = consult.Identifier;
                response.Company = objCompany;
                response.Role = objRole;
                response.Gender = objGender;
                response.TypeIdentification = objTypeIdentification;
                response.State = objState;
                response.Email = consult.Email;
                response.Name = consult.Name;
                response.LastName = consult.LastName;
                response.Identification = consult.Identification;
                response.Phone = consult.Phone;
                response.Address = consult.Address;

                response.File = await AllFileByUser(consult.Identifier);
            }

            return response;
        }

    }
}
