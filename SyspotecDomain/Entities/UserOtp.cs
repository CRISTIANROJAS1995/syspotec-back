using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class UserOtp
    {
        public int Id { get; set; }

        [Required]
        public int TypeOtpId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

    }
}
