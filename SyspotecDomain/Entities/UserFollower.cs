using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class UserFollower
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int UserIdFollower { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
