using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.IRepositories;
using SyspotecDomain.IServices;
using System.Text;
using System.Security.Cryptography;
using SyspotecDomain.Enums;
using SyspotecDomain.Dtos.User;
using System.Reflection;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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

        public async Task<ResponseApiDto> AuthenticateAsync(RequestLoginDto request)
        {
            request.Password = EncryptPassword(request.Password);

            var valid = await _jWTManagerRepository.AuthenticateAsync(request);
            return valid;
        }

        public async Task<ResponseApiDto?> Add(UserDto request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                //valid user activation
                //var userActivation = await _userActivationService.GetByEmailActivation(request.Email);
                //if (userActivation == null)
                //{
                //    response.Result = false;
                //    response.Message = "El usuario no esta activado.";
                //}
                //else
                //{
                    Guid obj = Guid.NewGuid();
                    User model = new User();

                    model.Identifier = obj.ToString();
                   // model.SubscriptionId = (int)SubscriptionEnum.Free;
                    model.GenderId = request.GenderId;
                    model.StateId = (int)StateEnum.Active;
                    model.Name = request.Name;
                    model.UserName = request.UserName;
                    model.Password = EncryptPassword(request.Password);
                    model.ArtistName = request.ArtistName;
                    model.Email = request.Email;
                    model.Nationality = request.Nationality;
                    model.CodeHfa = request.CodeHfa;
                    model.CodeNationality = request.CodeNationality;
                    model.BirthDate = request.BirthDate;
                    model.IsVerified = true;
                    model.DeviceToken = request.DeviceToken;
                    model.CreatedDate = DateTime.Now;
                    model.UpdateDate = DateTime.Now;
                    model.CoinAmount = 0;
                    model.PointAmount = 0;
                    model.IsMigration = false;

                    var responseAdd = await _userRepository.Add(model);
                    if (responseAdd == 1)
                    {
                        response.Result = true;

                        var consultUser = await _userRepository.GetByEmail(request.Email);
                        if (consultUser != null && request.Biography != null)
                        {
                           // await AddOrUpdateBiography(consultUser.Id, request.Biography);
                        }
                    }
                    else
                    {
                        response.Result = false;
                        response.Message = "Ocurrio un error inesperado al guardar el usuario.";
                    }
               // }
            }
            else
            {
                response.Result = false;
                response.Message = "El usuario ya se encuentra registrado.";
            }

            return response;
        }

        public async Task<ResponseApiDto?> Update(UserUpdateDto request, string identifier)
        {
            var response = new ResponseApiDto();

            var user = await GetIdByIdentifier(identifier);
            if (user == null)
            {
                response.Result = false;
                response.Message = "El usuario no existe.";
            }
            else
            {
                user.SubscriptionId = request.SubscriptionId > 0 ? request.SubscriptionId : user.SubscriptionId;
                user.GenderId = request.GenderId > 0 ? request.GenderId : user.GenderId;
                user.StateId = request.StateId > 0 ? request.StateId : user.StateId;
                user.Name = request.Name != null ? request.Name : user.Name;
                user.UserName = request.UserName != null ? request.UserName : user.UserName;
                user.Password = request.Password != null ? EncryptPassword(request.Password) : user.Password;
                user.ArtistName = request.ArtistName != null ? request.ArtistName : user.ArtistName;
                user.Email = request.StateId == (int)StateEnum.Inactive ? user.Email + "_Inactive" : user.Email;
                user.Nationality = request.Nationality != null ? request.Nationality : user.Nationality;
                user.CodeHfa = request.CodeHfa != null ? request.CodeHfa : user.CodeHfa;
                user.CodeNationality = request.CodeNationality != null ? request.CodeNationality : user.CodeNationality;
                user.BirthDate = (DateTime)(request.BirthDate != null ? request.BirthDate : user.BirthDate);
                user.IsVerified = request.IsVerified;
                user.DeviceToken = request.DeviceToken != null ? request.DeviceToken : user.DeviceToken;
                user.CreatedDate = user.CreatedDate;
                user.UpdateDate = DateTime.Now;
                user.CoinAmount = request.CoinAmount > 0 ? request.CoinAmount : user.CoinAmount;
                user.PointAmount = request.PointAmount > 0 ? request.PointAmount : user.PointAmount;

                var responseUpdate = await _userRepository.Update(user);
                if (responseUpdate == 1)
                {
                    response.Result = true;

                    //save biography
                    var consultUser = await GetIdByIdentifier(identifier);
                    if (consultUser != null && request.Biography != null)
                    {
                        //await AddOrUpdateBiography(consultUser.Id, request.Biography);
                    }
                }
                else
                {
                    response.Result = false;
                    response.Message = "Ocurrio un error inesperado al actualizar el usuario.";
                }
            }

            return response;
        }

        public async Task<ResponseApiDto?> GetByEmail(ValidEmailDto request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.GetByEmail(request.Email);
            if (user != null)
            {
                response.Result = true;
            }

            return response;
        }
        public async Task<ResponseApiDto?> GetByUserName(ValidUserNameDto request)
        {
            var response = new ResponseApiDto();

            var user = await _userRepository.GetByUserName(request.UserName);
            if (user != null)
            {
                response.Result = true;
            }

            return response;
        }

        public async Task<List<UserResponseDto>?> GetAll(PaginationDto pagination)
        {
            var list = await _userRepository.GetAll(pagination);
            return list;
        }

        public async Task<UserResponseDto> GetByIdentifier(string identifier)
        {
            var user = await _userRepository.GetByIdentifier(identifier);
            return user;
        }

        public async Task<List<UserResponseDto>?> GetAllFilterByArtistName(PaginationDto pagination, string name)
        {
            var list = await _userRepository.GetAllFilterByArtistName(pagination, name);
            return list;
        }

        public async Task<User?> GetIdByIdentifier(string identifier)
        {
            var user = await _userRepository.GetIdByIdentifier(identifier);
            return user;
        }

        public async Task<List<UserResponseSummaryDto>?> GetRanking()
        {
            var list = await _userRepository.GetRanking();
            return list;
        }

        public async Task<List<UserResponseSummaryDto>?> GetTopLastWeek()
        {
            var list = await _userRepository.GetTopLastWeek();
            return list;
        }
        public async Task<ResponseApiDto> AuthenticateForgotAsync(RequestLoginDto request)
        {
            request.Password = EncryptPassword(request.Password);

            var valid = await _jWTManagerRepository.AuthenticateForgotAsync(request);
            return valid;
        }

        //private async Task<ResponseApiDto?> AddOrUpdateBiography(int userId, UserBiographyDto request)
        //{
        //    UserBiography modelBiography = new UserBiography();
        //    modelBiography.UserId = userId;
        //    modelBiography.Description = request.Description;
        //    modelBiography.UrlFacebook = request.UrlFacebook;
        //    modelBiography.UrlInstagram = request.UrlInstagram;
        //    modelBiography.UrlSoundCloud = request.UrlSoundCloud;
        //    modelBiography.UrlSpotify = request.UrlSpotify;
        //    modelBiography.UrlWeb = request.UrlWeb;
        //    modelBiography.UrlYoutube = request.UrlYoutube;

        //    // return await _userBiographyService.AddOrUpdate(modelBiography);
        //    return modelBiography;
        //}


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
