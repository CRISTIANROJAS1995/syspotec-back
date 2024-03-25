using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class HiBeatDto
    {
        //public string Identifier { get; set; }

        //public int UserId { get; set; }

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

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        public List<InstrumentInterestDto> LstInstrument { get; set; }

        [Required]
        public List<MusicalInterestDto> LstMusicalInterest { get; set; }
    }
}
