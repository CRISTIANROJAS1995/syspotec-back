using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserUpdateDto
    {
        public int SubscriptionId { get; set; }

        public int GenderId { get; set; }

        public int StateId { get; set; }

        public string? Name { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? ArtistName { get; set; }

        public string? Email { get; set; }

        public string? Nationality { get; set; }

        public string? CodeHfa { get; set; }

        public string? CodeNationality { get; set; }

        public DateTime? BirthDate { get; set; } = null;

        public bool IsVerified { get; set; } = true;

        public string? DeviceToken { get; set; }

        public int CoinAmount { get; set; }

        public int PointAmount { get; set; }

        public bool IsMigration { get; set; } = false;

        public UserBiographyDto? Biography { get; set; }

        public string? identifier { get; set; }
    }
}
