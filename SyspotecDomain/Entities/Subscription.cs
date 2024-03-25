using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public int TypeSubscriptionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdateDate { get; set; }


        [ForeignKey("TypeSubscriptionId")]
        public TypeSubscription TypeSubscription { get; set; }

        public ICollection<User> User { get; set; }

    }
}
