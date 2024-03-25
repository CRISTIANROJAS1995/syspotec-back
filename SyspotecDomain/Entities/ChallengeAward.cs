using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class ChallengeAward
    {
        public int Id { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        [Required]
        public string UrlImage { get; set; }


        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }

    }
}
