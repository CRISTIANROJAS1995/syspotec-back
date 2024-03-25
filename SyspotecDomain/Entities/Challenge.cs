using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Challenge
    {
        public int Id { get; set; }

        public int PlayListId { get; set; }

        public int StateId { get; set; }

        [Required]
        public string Title { get; set; }

        public string UrlImage { get; set; }

        public string Description { get; set; }

        [Required]
        public string LegalBases { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("PlayListId")]
        public PlayList PlayList { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        public ICollection<ChallengeAward> ChallengeAward { get; set; }
        public ICollection<ChallengeWinner> ChallengeWinner { get; set; }
        public ICollection<ChallengeHiBeat> ChallengeHiBeat { get; set; }
    }
}
