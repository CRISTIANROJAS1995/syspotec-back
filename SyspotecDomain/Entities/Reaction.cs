using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Reaction
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TypeReactionId { get; set; }

        [Required]
        public int HiBeatId { get; set; }

        [Required]
        public int StateId { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsRead { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("TypeReactionId")]
        public TypeReaction TypeReaction { get; set; }

        [ForeignKey("HiBeatId")]
        public HiBeat HiBeat { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

    }
}
