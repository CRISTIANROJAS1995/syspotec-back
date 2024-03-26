using System.ComponentModel.DataAnnotations;

namespace SyspotecDomain.Input
{
    public class UserFileInput
    {
        [Required]
        public int TypeFileId { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
