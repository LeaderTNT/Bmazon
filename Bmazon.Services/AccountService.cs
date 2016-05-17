using Bmazon.Data;
using Bmazon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Services
{
    public class AccountService
    {
        private readonly string _email;
  
        public AccountService(string email)
        {
            _email = email;
        }

        public Customer InitializeCustomerAccount()
        {
            Customer entity;
            Cart initialCart;

            using (var ctx = new BmazonDbContext())
            {
                entity = new Customer{ Email = _email };
                initialCart = new Cart { CustomerID = _email };

                ctx.Accounts.Add(entity);
                ctx.Carts.Add(initialCart);
                ctx.SaveChanges();
            }

            return entity;
        }

        public Seller InitializeSellerAccount()
        {
            Seller entity;

            using (var ctx = new BmazonDbContext())
            {
                entity =
                    new Seller
                    {
                        Email = _email,
                        Company = _email
                    };

                ctx.Accounts.Add(entity);
                ctx.SaveChanges();
            }

            return entity;
        }

        public CustomerProfileModel GetCustomer()
        {
            Customer entity;

            using (var ctx = new BmazonDbContext())
            {
                entity = ctx.Customers.SingleOrDefault(e => e.Email == _email);
            }

            if (entity == null)
                entity = InitializeCustomerAccount();

            return
                new CustomerProfileModel
                {
                    Email = entity.Email,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,
                    ShippingAddress = entity.ShippingAddress,
                    BillingAddress = entity.BillingAddress
                };
        }

        public SellerProfileModel GetSeller()
        {
            Seller entity;

            using (var ctx = new BmazonDbContext())
            {
                entity =ctx.Sellers.SingleOrDefault(e => e.Email == _email);
            }

            if (entity == null)
                entity = InitializeSellerAccount();

            return
                new SellerProfileModel
                {
                    Email = entity.Email,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    PhoneNumber = entity.PhoneNumber,
                    Company = entity.Company,
                    Earning = entity.Earning
                };
        }

        public bool UpdateAccount(CustomerProfileModel vm)
        {
            using (var ctx = new BmazonDbContext())
            {
                var entity = ctx.Customers.SingleOrDefault(e => e.Email == vm.Email);

                entity.FirstName = vm.FirstName;
                entity.LastName = vm.LastName;
                entity.PhoneNumber = vm.PhoneNumber;
                entity.ShippingAddress = vm.ShippingAddress;
                entity.BillingAddress = vm.BillingAddress;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateAccount(SellerProfileModel vm)
        {
            using (var ctx = new BmazonDbContext())
            {
                var entity = ctx.Sellers.SingleOrDefault(e => e.Email == vm.Email);

                entity.FirstName = vm.FirstName;
                entity.LastName = vm.LastName;
                entity.PhoneNumber = vm.PhoneNumber;
                entity.Company = vm.Company;
                entity.Earning = vm.Earning;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
