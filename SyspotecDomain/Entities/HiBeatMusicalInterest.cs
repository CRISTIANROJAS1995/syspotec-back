using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class HiBeatMusicalInterest
    {
        public int Id { get; set; }

        [Required]
        public int HiBeatId { get; set; }

        [Required]
        public int MusicalInterestId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("HiBeatId")]
        public HiBeat HiBeat { get; set; }

        [ForeignKey("MusicalInterestId")]
        public MusicalInterest MusicalInterest { get; set; }

    }
}
