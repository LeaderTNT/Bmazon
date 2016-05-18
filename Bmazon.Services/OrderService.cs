using Bmazon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Services
{
    class OrderService
    {
        private readonly string _email;

        public OrderService(string email)
        {
            _email = email;
        }

        //public IEnumerable<Order> GetOrders()
        //{
        //    using (var ctx = new BmazonDbContext())
        //    {
        //        return
        //            ctx
        //                .Products
        //                .Where(e => e.Seller == _email)
        //                .Select(
        //                    e =>
        //                        new SellerProductModel
        //                        {
        //                            ProductID = e.ProductID,
        //                            Name = e.Name,
        //                            Description = e.Description,
        //                            AvailableNum = e.AvailableNum,
        //                            Price = e.Price,
        //                        })
        //                .ToArray();
        //    }
        //}
    }
}
