using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bmazon.Data;
using Bmazon.Services;
using Microsoft.AspNet.Identity;
using Bmazon.Models;

namespace Bmazon.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly Lazy<ProductService> _svcProduct;
        private readonly Lazy<ReviewService> _svcReview;
        public ProductController()
        {
            _svcProduct =
                new Lazy<ProductService>(
                    () =>
                    {
                        var email = User.Identity.GetUserName();
                        return new ProductService(email);
                    });

            _svcReview =
                new Lazy<ReviewService>(
                    () =>
                    {
                        var email = User.Identity.GetUserName();
                        return new ReviewService(email);
                    });
        }

        //Action For Seller Side

        public ActionResult SellerIndex(int? id)
        {
            CheckCustomer((bool)Session["isSeller"]);

            var viewModel = new ProductIndexData();
            viewModel.Products = _svcProduct.Value.GetProductsForSeller();

            if (id != null)
            {
                ViewBag.ProductID = id.Value;
                viewModel.AverageRating = _svcReview.Value.GetAverageRating(id.Value);
                viewModel.Reviews = _svcReview.Value.GetReviewsForProduct(id.Value);
            }

            return View(viewModel);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            CheckCustomer((bool)Session["isSeller"]);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SellerProductModel vm)
        {
            {
                if (ModelState.IsValid)
                {
                    if (!_svcProduct.Value.CreateProduct(vm))
                    {
                        ModelState.AddModelError("", "Unable to create note");
                        return View(vm);
                    }
                    return RedirectToAction("SellerIndex");
                }

                return RedirectToAction("SellerIndex");
            }
        }

        public ActionResult Edit(int? id)
        {
            CheckCustomer((bool)Session["isSeller"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detail = _svcProduct.Value.GetProductForSellerByID(id);
            if (detail == null)
            {
                return HttpNotFound();
            }

            var product =
                new SellerProductModel
                {
                    ProductID = detail.ProductID,
                    Name = detail.Name,
                    Description = detail.Description,
                    AvailableNum = detail.AvailableNum,
                    Price = detail.Price
                };

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SellerProductModel vm)
        {
            if (ModelState.IsValid)
            {
                if (!_svcProduct.Value.UpdateProduct(vm))
                {
                    ModelState.AddModelError("", "Unable to update note");
                    return View(vm);
                }
                return RedirectToAction("SellerIndex");
            }

            return RedirectToAction("SellerIndex");
        }

        public ActionResult Delete(int? id)
        {
            CheckCustomer((bool)Session["isSeller"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detail = _svcProduct.Value.GetProductForSellerByID(id);
            if (detail == null)
            {
                return HttpNotFound();
            }

            var product =
                new SellerProductModel
                {
                    ProductID = detail.ProductID,
                    Name = detail.Name,
                    Description = detail.Description,
                    AvailableNum = detail.AvailableNum,
                    Price = detail.Price
                };


            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                _svcProduct.Value.DeleteProduct(id);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                ModelState.AddModelError("Error", e.Message);
                return View();
            }

            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message + "\nUnable to update profile");
                return View();
            }

            return RedirectToAction("SellerIndex");
        }

        //Action For Customer Side

        private void CheckSeller(bool isSeller)
        {
            if (isSeller)
                Response.Write("<script language='javascript'>window.alert('You need to change to a Customer Mode to access this page!'); window.location='/Manage/Index';</script>");
        }

        private void CheckCustomer(bool isSeller)
        {
            if (!isSeller)
                Response.Write("<script language='javascript'>window.alert('You need to change to a Seller Mode to access this page!'); window.location='/Manage/Index';</script>");
        }
    }
}