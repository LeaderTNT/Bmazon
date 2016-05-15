using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class Cart
    {
        public string CustomerID { get; set; }

        public double TotalPayment { get; set; }

        public IEnumerable<BoughtProduct> BoughtProducts { get; set; }
    }
}
