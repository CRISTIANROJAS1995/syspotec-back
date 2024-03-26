using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class UserContract
    {
        public int Id { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public string UserAssign { get; set; }   

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

    }
}
