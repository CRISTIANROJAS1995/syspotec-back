using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Home
{
    public class PlayListDto : PaginationDto
    {
        public string Filter { get; set; }
    }
}
