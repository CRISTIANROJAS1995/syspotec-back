using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> User { get; set; }
        public ICollection<UserImage> UserImage { get; set; }
        public ICollection<UserMap> UserMap { get; set; }
        public ICollection<HiBeat> HiBeat { get; set; }
    }


}
