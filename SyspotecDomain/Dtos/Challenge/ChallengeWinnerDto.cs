using SyspotecDomain.Dtos.Hibeat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Challenge
{
    public class ChallengeWinnerDto
    {
        public int Position { get; set; }
        public int Points { get; set; }
        public HibeatResponseDto Hibeat { get; set; }
    }
}
