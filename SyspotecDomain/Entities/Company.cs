using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Nit { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        //public ICollection<User> User { get; set; }
    }
}
