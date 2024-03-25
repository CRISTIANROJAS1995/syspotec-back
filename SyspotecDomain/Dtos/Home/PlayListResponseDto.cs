using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.User;

namespace SyspotecDomain.Dtos.Home
{
    public class PlayListResponseDto
    {
        public TypePlayListDto TypePlayList { get; set; }

        public StateDto State { get; set; }

        public string CoverImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public List<HibeatResponseDto> lstHibeat { get; set; }
    }
}
