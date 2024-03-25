using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class ReactionResponseDto
    {
        public int Id { get; set; }

        public UserResponseSummaryDto User { get; set; }

        public TypeReactionDto TypeReaction { get; set; }

        public HibeatResponseDto? HiBeat { get; set; }

        public StateDto State { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
