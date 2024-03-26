using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyspotecDomain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        public int TypeIdentificationId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Identification { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        [ForeignKey("TypeIdentificationId")]
        public TypeIdentification TypeIdentification { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        public ICollection<UserContract> UserContract { get; set; }
    }
}
