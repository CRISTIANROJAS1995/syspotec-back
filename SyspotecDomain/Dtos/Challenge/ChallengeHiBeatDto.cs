using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Hibeat;

namespace SyspotecDomain.Dtos.Challenge
{
    public class ChallengeHiBeatDto
    {
        public HibeatResponseDto Hibeat { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Points { get; set; }

    }
}
