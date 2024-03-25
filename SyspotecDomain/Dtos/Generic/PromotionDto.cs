using SyspotecDomain.Dtos.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Generic
{
    public class PromotionDto
    {
        public string TitleEnglish { get; set; }

        public string TitleSpanish { get; set; }

        [Required]
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
