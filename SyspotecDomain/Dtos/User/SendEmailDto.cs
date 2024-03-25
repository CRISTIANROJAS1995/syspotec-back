using SyspotecDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class SendEmailOtpDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public TypeOtpEnum Type { get; set; }
    }
}
