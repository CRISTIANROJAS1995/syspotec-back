using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SyspotecDomain.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int TypeFileId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        [ForeignKey("TypeFileId")]
        public TypeFile TypeFile { get; set; }

        public ICollection<UserContract> UserContract { get; set; }

    }
}
