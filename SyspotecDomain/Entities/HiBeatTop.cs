using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class HiBeatTop
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public int UserId { get; set; }

        public int StateId { get; set; }

        public string Title { get; set; }

        public string Tone { get; set; }

        public string Duration { get; set; }

        public string Bpm { get; set; }

        public string RecordCompany { get; set; }

        public string UrlFile { get; set; }

        public string UrlCover { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int Points { get; set; }

    }
}
