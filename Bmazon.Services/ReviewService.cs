using Bmazon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bmazon.Services
{
    public class ReviewService
    {
        private readonly string _email;

        public ReviewService(string email)
        {
            _email = email;
        }

        public IEnumerable<Review> GetReviewsForProduct(int ProductID)
        {
            using (var ctx = new BmazonDbContext())
            {
                string query = "SELECT * FROM Review WHERE ProductID = @p0";
                IEnumerable<Review> reviews = ctx.Reviews.SqlQuery(query, ProductID).ToArray();

                return reviews;
            }
        }

        public IEnumerable<Review> GetReviewsForSeller(string SellerEmail)
        {
            using (var ctx = new BmazonDbContext())
            {
                string query = "SELECT * FROM Review WHERE SellerEmail = @p0";
                IEnumerable<Review> reviews = ctx.Reviews.SqlQuery(query, SellerEmail).ToArray();

                return reviews;
            }
        }

        public double GetProductAverageRating(int productID)
        {
            using (var ctx = new BmazonDbContext())
            {
                try
                {
                    double rating = ctx.Reviews.Where(r => r.ProductID == productID).Select(r => r.Rating).Average();
                    return rating;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public double GetSellerAverageRating(string email)
        {
            using (var ctx = new BmazonDbContext())
            {
                try
                {
                    double rating = ctx.Reviews.Where(r => r.SellerEmail == email).Select(r => r.Rating).Average();
                    return rating;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string ConvertToCampanyName(string email)
        {
            using (var ctx = new BmazonDbContext())
            {
                return ctx.Sellers.SingleOrDefault(s => s.Email == _email).Company;
            }
        }
    }
}
