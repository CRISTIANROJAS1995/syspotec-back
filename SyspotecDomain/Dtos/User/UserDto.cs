using SyspotecDomain.Dtos.Generic;
using System;

namespace SyspotecDomain.Dtos.User
{
    public class UserDto
    {
        public string Identifier { get; set; }

        public CompanyDto Company { get; set; }

        public RoleDto Role { get; set; }

        public GenderDto Gender { get; set; }

        public TypeIdentificationDto TypeIdentification { get; set; }

        public StateDto State { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
