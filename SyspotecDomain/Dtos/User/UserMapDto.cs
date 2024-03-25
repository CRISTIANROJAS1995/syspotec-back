using SyspotecDomain.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.User
{
    public class UserMapDto
    {
        public StateDto? State { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Location { get; set; }

    }
}
