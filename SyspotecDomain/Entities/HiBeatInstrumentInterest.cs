using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class HiBeatInstrumentInterest
    {
        public int Id { get; set; }

        [Required]
        public int HiBeatId { get; set; }

        [Required]
        public int InstrumentInterestId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("HiBeatId")]
        public HiBeat HiBeat { get; set; }

        [ForeignKey("InstrumentInterestId")]
        public InstrumentInterest InstrumentInterest { get; set; }

    }
}
