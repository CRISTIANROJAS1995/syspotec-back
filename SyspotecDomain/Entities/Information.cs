using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Information
    {
        public int Id { get; set; }

        public string TitleEnglish { get; set; }

        public string TitleSpanish { get; set; }

        public string Text1English { get; set; }

        public string Text1Spanish { get; set; }

        public string Text2English { get; set; }

        public string Text2Spanish { get; set; }

        public string SubtitleEnglish { get; set; }

        public string SubtitleSpanish { get; set; }

        [Required]
        public string UrlOutstandingImage { get; set; }

        public string UrlSecondaryImage { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
