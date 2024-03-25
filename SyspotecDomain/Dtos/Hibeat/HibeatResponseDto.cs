using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class HibeatResponseDto
    {
        public string Identifier { get; set; }

        public UserResponseSummaryDto? User { get; set; }

        public StateDto State { get; set; }

        public string Title { get; set; }

        public string Tone { get; set; }

        public string Duration { get; set; }

        public string Bpm { get; set; }

        public string RecordCompany { get; set; }

        public string UrlFile { get; set; }

        public string UrlCover { get; set; }

        public int Points { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<InstrumentInterestDto> LstInstrument { get; set; }

        public List<MusicalInterestDto> LstMusicalInterest { get; set; }

        public List<ReactionResponseDto> LstReaction { get; set; }

        public HiBeatStatsDto? Stats { get; set; }

    }
}
