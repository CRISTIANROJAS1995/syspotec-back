using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.Dtos.Generic
{
    public class SubscriptionDto
    {
        public int Id { get; set; }

        //public int TypeSubscriptionId { get; set; }

        public TypeSubscriptionDto TypeSubscription { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
