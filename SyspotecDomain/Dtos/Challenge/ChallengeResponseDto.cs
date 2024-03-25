using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Challenge
{
    public class ChallengeResponseDto
    {
        public string Title { get; set; }

        public string UrlImage { get; set; }

        public string Description { get; set; }

        public string LegalBases { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public StateDto State { get; set; }

        public List<ChallengeAwardDto> LstAward { get; set; }

        public List<ChallengeWinnerDto> LstWinner { get; set; }

        public List<HibeatResponseDto> LstHibeat { get; set; }
    }
}
