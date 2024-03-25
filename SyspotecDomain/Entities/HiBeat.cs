using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class HiBeat
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        [Required]
        public int UserId { get; set; }

        public int StateId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Tone { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string Bpm { get; set; }

        public string RecordCompany { get; set; }

        [Required]
        public string UrlFile { get; set; }

        [Required]
        public string UrlCover { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        public ICollection<HiBeatMusicalInterest> HiBeatMusicalInterest { get; set; }
        public ICollection<HiBeatInstrumentInterest> HiBeatInstrumentInterest { get; set; }
        public ICollection<Reaction> Reaction { get; set; }
        public ICollection<ChallengeWinner> ChallengeWinner { get; set; }
        public ICollection<ChallengeHiBeat> ChallengeHiBeat { get; set; }

    }
}
