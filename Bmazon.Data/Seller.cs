using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bmazon.Data
{
    public class Seller : Account
    {
        [Index("CompanyIndex", IsUnique = true)]
        public string Company { get; set; }

        public double Earning { get; set; }

        public virtual IEnumerable<Review> Reviews { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
