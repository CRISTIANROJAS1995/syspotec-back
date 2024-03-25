using SyspotecDomain.Dtos.User;

namespace SyspotecDomain.Extension
{
    public class Tokens
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public UserDto UserData { get; set; }
    }
}
