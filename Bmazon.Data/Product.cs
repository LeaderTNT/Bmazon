using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class Product
    {
        public int ProductID { get; set; }

        public string Seller { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int AvailableNum { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "The price cannot be negative")]
        [DisplayFormat(DataFormatString = "{{0:#.##}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        public virtual IEnumerable<Review> Reviews { get; set; }
    }
}
