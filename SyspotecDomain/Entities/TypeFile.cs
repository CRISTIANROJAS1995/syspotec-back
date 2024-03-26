using System.ComponentModel.DataAnnotations;


namespace SyspotecDomain.Entities
{
    public class TypeFile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Contract> Contract { get; set; }
        public ICollection<UserFile> UserFile { get; set; }
    }
}
