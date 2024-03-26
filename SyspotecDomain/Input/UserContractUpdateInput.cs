using System.ComponentModel.DataAnnotations;

namespace SyspotecDomain.Input
{
    public class UserContractUpdateInput
    {
        [Required]
        public string ContractId { get; set; }

        [Required]
        public int StateId { get; set; }
    }
}
