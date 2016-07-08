using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bmazon.Data;
using Microsoft.AspNet.Identity;
using Bmazon.Services;
using Bmazon.Models;

namespace Bmazon.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly Lazy<ReviewService> _svc;

        public ReviewController()
        {
            _svc =
                new Lazy<ReviewService>(
                    () =>
                    {
                        var email = User.Identity.GetUserName();
                        return new ReviewService(email);
                    });
        }

        //Action For Seller Side

        public ActionResult SellerIndex()
        {
            CheckCustomer((bool)Session["isSeller"]);

            var viewModel = new ReviewViewModel();

            viewModel.Reviews = _svc.Value.GetReviewsForSeller(User.Identity.GetUserName());
            viewModel.AverageRating = _svc.Value.GetSellerAverageRating(User.Identity.GetUserName());
            viewModel.Company = _svc.Value.ConvertToCampanyName(User.Identity.GetUserName());
            return View(viewModel);
        }


        //// GET: Review/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = db.Reviews.Find(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(review);
        //}

        //// GET: Review/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Seller");
        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName");
        //    return View();
        //}

        //// POST: Review/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CustomerEmail,Company,ProductID,Rating,Comment")] Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Reviews.Add(review);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Seller", review.ProductID);
        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName", review.CustomerEmail);
        //    return View(review);
        //}

        //// GET: Review/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = db.Reviews.Find(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Seller", review.ProductID);
        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName", review.CustomerEmail);
        //    return View(review);
        //}

        //// POST: Review/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,CustomerEmail,Company,ProductID,Rating,Comment")] Review review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(review).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Seller", review.ProductID);
        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName", review.CustomerEmail);
        //    return View(review);
        //}

        //// GET: Review/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Review review = db.Reviews.Find(id);
        //    if (review == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(review);
        //}

        //// POST: Review/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Review review = db.Reviews.Find(id);
        //    db.Reviews.Remove(review);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
