using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Hibeat
{
    public class ReactionDto
    {
        //[Required]
        //public string UserId { get; set; }

        [Required]
        public int TypeReactionId { get; set; }

        [Required]
        public string HiBeatId { get; set; }

        public int StateId { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }
    }
}
