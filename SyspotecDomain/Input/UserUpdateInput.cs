﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Input
{
    public class UserUpdateInput
    {
        public string? CompanyId { get; set; }

        public int RoleId { get; set; }

        public int GenderId { get; set; }

        public int TypeIdentificationId { get; set; }

        public int StateId { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Identification { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }
}
