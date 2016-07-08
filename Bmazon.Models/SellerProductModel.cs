using Bmazon.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Models
{
    public class SellerProductModel
    {
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, 500, ErrorMessage = "The Available Number should be between 1 and 500")]
        [Display(Name = "Available Number")]
        public int AvailableNum { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "The price cannot be negative")]
        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual IEnumerable<Review> Reviews { get; set; }
    }
}
