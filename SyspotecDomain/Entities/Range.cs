using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Range
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int MinPoint { get; set; }

        [Required]
        public int MaxPoint { get; set; }

        [Required]
        public int Coin { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
