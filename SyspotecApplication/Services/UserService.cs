using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System.Text;
using System.Security.Cryptography;
using SyspotecDomain.Enums;
//using SyspotecDomain.Dtos.User;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using SyspotecDomain.Input;
using System.Numerics;
using System.Net;
using SyspotecDomain.Dtos.User;

namespace SyspotecApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTManagerRepository _jWTManagerRepository;

        public UserService(
            IUserRepository userRepository,
            IJWTManagerRepository jWTManagerRepository)
        {
            _userRepository = userRepository;
            _jWTManagerRepository = jWTManagerRepository;
        }

        public async Task<ResponseApiDto> Authenticate(LoginInput request)
        {
            request.Password = EncryptPassword(request.Password);
            return await _jWTManagerRepository.Authenticate(request);
        }

        public async Task<ResponseApiDto?> Add(UserInput request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByEmail(request.Email);
            if (user == null)
            {
                var consultCompany = await _userRepository.CompanyByIdentifier(request.CompanyId);
                if (consultCompany != null)
                {
                    Guid obj = Guid.NewGuid();
                    User model = new()
                    {
                        Identifier = obj.ToString(),
                        CompanyId = consultCompany.Id,
                        RoleId = request.RoleId,
                        GenderId = request.GenderId,
                        TypeIdentificationId = request.TypeIdentificationId,
                        StateId = (int)StateEnum.Active,
                        Email = request.Email,
                        Password = EncryptPassword(request.Password),
                        Name = request.Name,
                        LastName = request.LastName,
                        Identification = request.Identification,
                        Phone = request.Phone,
                        Address = request.Address,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now
                    };

                    var responseAdd = await _userRepository.Add(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar el usuario.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "La compañía no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(UserUpdateInput request, string identifier)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByIdentifier(identifier);
            if (user == null)
            {
                response.Result = false;
                response.Message = "El usuario no existe.";
            }
            else
            {
                user.RoleId = request.RoleId > 0 ? request.RoleId : user.RoleId;
                user.GenderId = request.GenderId > 0 ? request.GenderId : user.GenderId;
                user.TypeIdentificationId = request.TypeIdentificationId > 0 ? request.TypeIdentificationId : user.TypeIdentificationId;
                user.StateId = request.StateId > 0 ? request.StateId : user.StateId;
                user.Password = request.Password != null ? EncryptPassword(request.Password) : user.Password;
                user.Name = request.Name != null ? request.Name : user.Name;
                user.LastName = request.LastName != null ? request.LastName : user.LastName;
                user.Identification = request.Identification != null ? request.Identification : user.Identification;
                user.Phone = request.Phone != null ? request.Phone : user.Phone;
                user.Address = request.Address != null ? request.Address : user.Address;
                user.UpdateDate = DateTime.Now;

                var responseUpdate = await _userRepository.Update(user);
                if (responseUpdate == 1)
                {
                    response.Result = true;
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar el usuario.";
                }
            }

            return response;
        }

        public async Task<List<UserDto>?> All()
        {
            return await _userRepository.All();
        }

        public async Task<UserDto> ByIdentifierDto(string identifier)
        {
            return await _userRepository.ByIdentifierDto(identifier);
        }

        public async Task<User?> ByIdentifier(string identifier)
        {
            return await _userRepository.ByIdentifier(identifier);
        }

        public async Task<User?> ByEmail(string email)
        {
            return await _userRepository.ByEmail(email);
        }

        public async Task<Company?> CompanyByIdentifier(string identifier)
        {
            return await _userRepository.CompanyByIdentifier(identifier);
        }

        #region UserFile

        public async Task<ResponseApiDto?> AddUserFile(UserFileInput request, string userId)
        {
            var response = new ResponseApiDto();

            var userFile = await _userRepository.UserFileByType(request.TypeFileId);
            if (userFile == null)
            {
                var consultUser = await ByIdentifier(userId);
                if (consultUser != null)
                {
                    UserFile model = new()
                    {
                        UserId = consultUser.Id,
                        StateId = (int)StateEnum.Active,
                        TypeFileId = request.TypeFileId,
                        Url = request.Url,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                    };

                    var responseAdd = await _userRepository.AddUserFile(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar el archivo.";
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "El usuario no existe.";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario ya tiene guardado ese tiene de archivo.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> UpdateUserFile(UserFileUpdateInput request, string identifier)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.ByIdentifier(identifier);
            if (user == null)
            {
                response.Result = false;
                response.Message = "El usuario no existe.";
            }
            else
            {
                var userFile = await _userRepository.UserFileByType(request.TypeFileId);
                if (userFile == null)
                {
                    response.Result = false;
                    response.Message = "El usuario no tiene asignado este tipo de arhcivo";
                }
                else
                {
                    userFile.Url = request.Url != null ? request.Url : user.Name;
                    userFile.UpdateDate = DateTime.Now;

                    var responseUpdate = await _userRepository.UpdateUserFile(userFile);
                    if (responseUpdate == 1)
                    {
                        response.Result = true;
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al actualizar el archivo.";
                    }
                }
            }

            return response;
        }

        public async Task<List<UserFileDto>?> AllFileByUser(string userId)
        {
            return await _userRepository.AllFileByUser(userId);
        }

        #endregion

        private static string EncryptPassword(string password)
        {
            var md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(t.ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
