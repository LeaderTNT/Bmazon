using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public class Review
    {
        public int ID { get; set; }

        public string Reviewer { get; set; }

        public int ProductID { get; set; }

        public string SellerEmail { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        public string Comment { get; set; }

    }
}
