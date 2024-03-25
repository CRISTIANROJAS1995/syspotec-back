using SyspotecDomain.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserResponseSummaryDto
    {
        public string Identifier { get; set; }

        //public int SubscriptionId { get; set; }

        public SubscriptionDto Subscription { get; set; }

        //public int GenderId { get; set; }

        public GenderDto Gender { get; set; }

        //public int StateId { get; set; }

        public StateDto State { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string ArtistName { get; set; }

        public string Email { get; set; }

        public string Nationality { get; set; }

        public string CodeHfa { get; set; }

        public string CodeNationality { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsVerified { get; set; }

        public string DeviceToken { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int CoinAmount { get; set; }

        public int PointAmount { get; set; }

        public bool IsMigration { get; set; }

        public List<UserImageDto> ListImage { get; set; }

        public UserMapDto? Map { get; set; } = null;

    }
}
