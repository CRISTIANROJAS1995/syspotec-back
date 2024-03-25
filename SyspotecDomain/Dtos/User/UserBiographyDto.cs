using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserBiographyDto
    {
        public string Description { get; set; }

        public string UrlFacebook { get; set; }

        public string UrlInstagram { get; set; }

        public string UrlSoundCloud { get; set; }

        public string UrlSpotify { get; set; }

        public string UrlWeb { get; set; }

        public string UrlYoutube { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
