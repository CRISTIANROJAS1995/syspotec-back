using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class UserBiography
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Description { get; set; }

        public string UrlFacebook { get; set; }

        public string UrlInstagram { get; set; }

        public string UrlSoundCloud { get; set; }

        public string UrlSpotify { get; set; }

        public string UrlWeb { get; set; }

        public string UrlYoutube { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
