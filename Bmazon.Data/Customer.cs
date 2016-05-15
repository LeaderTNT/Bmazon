using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Bmazon.Data
{
    public class Customer : Account
    {
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        public Cart Cart { get; set; }
    
        public virtual ICollection<Review> Reviews { get; set;}

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<PaymentOption> PaymentOptions { get; set; }

    }
}
