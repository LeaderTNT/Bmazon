using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bmazon.Data;

namespace Bmazon.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private BmazonDbContext db = new BmazonDbContext();

        // GET: Order
        public ActionResult SellerIndex()
        {
            CheckCustomer((bool)Session["isSeller"]);
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Seller);
            return View(orders.ToList());
        }

        //// GET: Order/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// GET: Order/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName");
        //    ViewBag.SellerEmail = new SelectList(db.Accounts, "Email", "FirstName");
        //    return View();
        //}

        //// POST: Order/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "OrderID,CustomerEmail,SellerEmail,TotalPayment")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Orders.Add(order);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.CustomerEmail = new SelectList(db.Accounts, "Email", "FirstName", order.CustomerEmail);
        //    ViewBag.SellerEmail = new SelectList(db.Accounts, "Email", "FirstName", order.SellerEmail);
        //    return View(order);
        //}

        //// GET: Order/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Order/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

       [System.Web.Services.WebMethod]
        public bool UpdateStatus(int id, int statusValue)
        {
            using (var ctx = new BmazonDbContext())
            {
                var result = ctx.Database.ExecuteSqlCommand($"UPDATE [Order] SET Status = {statusValue} WHERE OrderID = {id}");

                return result == 1;
            }
        }
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
