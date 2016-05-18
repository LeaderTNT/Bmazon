using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class BoughtProduct : Product
    {
        public string Buyer { get; set; }

        [Range(1, 100, ErrorMessage = "The quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        public Order Order { get; set; }
    }
}
