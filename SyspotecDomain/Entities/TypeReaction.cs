using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class TypeReaction
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Point { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public ICollection<Reaction> Reaction { get; set; }

    }
}
