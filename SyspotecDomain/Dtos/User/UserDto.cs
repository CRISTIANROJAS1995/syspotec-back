using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserDto
    {
        public string Identifier { get; set; }

        public int SubscriptionId { get; set; }

        [Required]
        public int GenderId { get; set; }

        public int StateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        public string ArtistName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Nationality { get; set; }

        public string CodeHfa { get; set; }

        public string CodeNationality { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public bool IsVerified { get; set; }

        public string DeviceToken { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int CoinAmount { get; set; }

        public int PointAmount { get; set; }

        public bool IsMigration { get; set; }

        public UserBiographyDto? Biography { get; set; }

    }
}
