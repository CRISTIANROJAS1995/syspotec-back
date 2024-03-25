using System.ComponentModel.DataAnnotations;

namespace SyspotecDomain.Input
{
    public class UserInput
    {
        [Required]
        public string CompanyId { get; set; }

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
    }
}
