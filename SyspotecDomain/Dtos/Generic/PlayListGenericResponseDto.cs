using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Generic
{
    public class PlayListGenericResponseDto
    {
        //public TypePlayListDto TypePlayList { get; set; }

        public StateDto State { get; set; }

        public string CoverImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

    }
}
