using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class RequestUserBlockDto
    {
        [Required]
        public string UserBlock { get; set; }
    }
}
