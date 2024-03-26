using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;

namespace SyspotecDomain.Dtos.Contract
{
    public class UserContractDto
    {
        public int Id { get; set; }

        public ContractDto Contract { get; set; }

        public StateDto State { get; set; }

        public UserDto User { get; set; }

        public UserDto UserAssign { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
