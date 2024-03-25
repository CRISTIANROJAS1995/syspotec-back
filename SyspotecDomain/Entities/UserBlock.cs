using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class UserBlock
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int UserIdBlock { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
