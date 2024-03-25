using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Challenge
{
    public class ChallengeDto
    {
        public string Title { get; set; }

        public string UrlImage { get; set; }

        public string Description { get; set; }

        public string LegalBases { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
