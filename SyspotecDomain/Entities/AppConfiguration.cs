using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class AppConfiguration
    {
        public int Id { get; set; }

        [Required]
        public bool SeverMaintenance { get; set; }

        [Required]
        public string VersionApp { get; set; }

        public string UrlFaqs { get; set; }

        public string UrlPolicy { get; set; }

        public string UrlTerms { get; set; }

        public string UrlTermsChallenge { get; set; }

        public string UrlTermsMap { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
