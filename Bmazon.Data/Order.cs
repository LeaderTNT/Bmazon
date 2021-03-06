﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Data
{
    public enum Status
    {
        Pending,
        Processed,
        Shipped,
        Delivered
    }
    public class Order
    {
        public int OrderID { get; set; }

        public string CustomerEmail { get; set; }

        public string SellerEmail { get; set; }

        [DisplayFormat(DataFormatString = "{0:$0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Total Payment")]
        public double TotalPayment { get; set; }
        [Required]
        [DefaultValue(0)]
        public Status Status { get; set; }

        public virtual IEnumerable<BoughtProduct> BoughtProducts { get; set; }

        public Customer Customer { get; set; }

        public Seller Seller { get; set; }
    }
}
