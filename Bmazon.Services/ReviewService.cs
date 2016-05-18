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
        private readonly string _company;

        public ReviewService(string email)
        {
            _email = email;
            using (var ctx = new BmazonDbContext()) {
                _company = ctx.Sellers.SingleOrDefault(s => s.Email == _email).Company;
            }
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
                string query = "SELECT * FROM Review WHERE Seller_Email = @p0";
                IEnumerable<Review> reviews = ctx.Reviews.SqlQuery(query, SellerEmail).ToArray();

                return reviews;
            }
        }

        public double GetAverageRating(int ProductID)
        {
            using (var ctx = new BmazonDbContext())
            {
                double rating = ctx.Reviews.Select(r => r.Rating).Average();

                return rating;
            }
        }
    }
}
