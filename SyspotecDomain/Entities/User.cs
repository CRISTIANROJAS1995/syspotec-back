using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public int SubscriptionId { get; set; }

        [Required]
        public int GenderId { get; set; }

        public int StateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
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

        [Required]
        public int CoinAmount { get; set; }

        [Required]
        public int PointAmount { get; set; }

        public bool IsMigration { get; set; }


        [ForeignKey("SubscriptionId")]
        public Subscription Subscription { get; set; }

        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        public ICollection<UserBiography> UserBiography { get; set; }
        public ICollection<UserBlock> UserBlock { get; set; }
        public ICollection<UserFollower> UserFollower { get; set; }
        public ICollection<UserImage> UserImage { get; set; }
        public ICollection<UserMap> UserMap { get; set; }
        public ICollection<Reaction> Reaction { get; set; }
        public ICollection<UserDailyAchievement> UserDailyAchievement { get; set; }
    }
}
