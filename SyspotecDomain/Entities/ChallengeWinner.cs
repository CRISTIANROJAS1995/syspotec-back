using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class ChallengeWinner
    {
        public int Id { get; set; }

        [Required]
        public int ChallengeId { get; set; }

        [Required]
        public int HibeatId { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public int Points { get; set; }


        [ForeignKey("ChallengeId")]
        public Challenge Challenge { get; set; }

        [ForeignKey("HibeatId")]
        public HiBeat HiBeat { get; set; }
    }
}
