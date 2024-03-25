using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.Extension;
using SyspotecDomain.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyspotecDal.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration iconfiguration;

        public JWTManagerRepository(IConfiguration iconfiguration, IUserRepository userRepository)
        {
            this.iconfiguration = iconfiguration;
            _userRepository = userRepository;
        }

        public async Task<ResponseApiDto> AuthenticateAsync(RequestLoginDto request)
        {
            var response = new ResponseApiDto();
            var valid = await _userRepository.GetValidCredentialAsync(request);

            if (valid.Identifier == null)
            {
                response.Result = false;
                response.Message = "Combinación incorrecta de nombre de usuario y contraseña.";
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.NameIdentifier, valid.Identifier),
                new Claim(ClaimTypes.Name, valid.Name),
                new Claim(ClaimTypes.Email, request.Email)
              }),
                Expires = DateTime.UtcNow.AddMinutes(1440), //24hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            response.Result = true;
            response.Data = new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = DateTime.UtcNow.AddMinutes(1440).ToString(), UserData = valid };

            return response;

        }
        public async Task<ResponseApiDto> AuthenticateForgotAsync(RequestLoginDto request)
        {
            var response = new ResponseApiDto();
            var valid = await _userRepository.GetValidCredentialForgotAsync(request);

            if (valid.Identifier == null)
            {
                response.Result = false;
                response.Message = "Correo incorrecto";
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.NameIdentifier, valid.Identifier),
                new Claim(ClaimTypes.Name, valid.Name),
                new Claim(ClaimTypes.Email, request.Email)
              }),
                Expires = DateTime.UtcNow.AddMinutes(1440), //24hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            response.Result = true;
            response.Data = new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = DateTime.UtcNow.AddMinutes(1440).ToString(), UserData = valid };

            return response;

        }
    }
}
