using System.ComponentModel.DataAnnotations;

namespace SyspotecDomain.Input
{
    public class UserContractInput
    {
        [Required]
        public string ContractId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserAssign { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

}
