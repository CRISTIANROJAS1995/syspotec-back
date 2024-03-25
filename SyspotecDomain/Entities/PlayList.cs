using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class PlayList
    {
        public int Id { get; set; }

        public int TypePlayListId { get; set; }

        public int StateId { get; set; }

        public string CoverImage { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("TypePlayListId")]
        public TypePlayList TypePlayList { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        public ICollection<Challenge> Challenge { get; set; }

    }
}

