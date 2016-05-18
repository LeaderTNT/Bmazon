using Bmazon.Data;
using Bmazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Services
{
    public class ProductService
    {
        private readonly string _email;
        private readonly string _company;

        public ProductService(string email)
        {
            _email = email;
            using (var ctx = new BmazonDbContext())
            {
                _company = ctx.Sellers.SingleOrDefault(s => s.Email == _email).Company;
            }
        }
            

        public IEnumerable<SellerProductModel> GetProductsForSeller()
        {
            using (var ctx = new BmazonDbContext())
            {
                return
                    ctx
                        .Products
                        .Where(e => e.Seller == _company)
                        .Select(
                            e =>
                                new SellerProductModel
                                {
                                    ProductID = e.ProductID,
                                    Name = e.Name,
                                    Description = e.Description,
                                    AvailableNum = e.AvailableNum,
                                    Price = e.Price,
                                })
                        .ToArray();
            }
        }

        public SellerProductModel GetProductForSellerByID(int? id)
        {
            Product entity;

            using (var ctx = new BmazonDbContext())
            {
                entity = ctx.Products.SingleOrDefault(e => e.ProductID == id);
            }
            if (entity != null)
                return new SellerProductModel
                {
                    ProductID = entity.ProductID,
                    Name = entity.Name,
                    Description = entity.Description,
                    AvailableNum = entity.AvailableNum,
                    Price = entity.Price
                };
            else
                return null;    
         }

        public bool CreateProduct(SellerProductModel vm)
        {
            using (var ctx = new BmazonDbContext())
            {
                var entity =
                    new Product
                    {
                        Seller = _company,
                        Name = vm.Name,
                        Description = vm.Description,
                        AvailableNum = vm.AvailableNum,
                        Price = vm.Price
                    };

                ctx.Products.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateProduct(SellerProductModel vm)
        {
            using (var ctx = new BmazonDbContext())
            {
                var entity = ctx.Products.SingleOrDefault(e => e.ProductID == vm.ProductID);

                entity.Name = vm.Name;
                entity.Description = vm.Description;
                entity.Price = vm.Price;
                entity.AvailableNum = vm.AvailableNum;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteProduct(int? id)
        {
            using (var ctx = new BmazonDbContext())
            {
                var entity = ctx.Products.SingleOrDefault(e => e.ProductID == id);

                // TODO: Handle note not found

                ctx.Products.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
