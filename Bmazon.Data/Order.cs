using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class Order
    {
        public int OrderID { get; set; }

        public string Buyer { get; set; }

        public string Seller { get; set; }

        public double TotalPayment { get; set; }

        public virtual IEnumerable<BoughtProduct> BoughtProducts { get; set; }

        public PaymentOption PaymentOption { get; set; }
    }
}
