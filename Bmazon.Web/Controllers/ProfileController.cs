using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bmazon.Services;
using Bmazon.Models;
using Bmazon.Data;

namespace Bmazon.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly Lazy<AccountService> _svc;

        public ProfileController()
        {
            _svc =
                new Lazy<AccountService>(
                    () =>
                    {
                        var email = User.Identity.GetUserName();
                        return new AccountService(email);
                    });
        }

        // GET: Profile
        public ActionResult Index()
        {
            if ((bool)Session["isSeller"])
            {
                var profile = _svc.Value.GetSeller();
                return View("SellerIndex", profile);
            }
            else
            {
                var profile = _svc.Value.GetCustomer();
                return View("CustomerIndex", profile);
            }

        }

        public ActionResult EditSeller()
        {
            if (!(bool)Session["isSeller"])
            {
                Response.Write("<script language='javascript'>window.alert('You need to change to a Seller Mode to access this page!'); window.location='../Manage/Index';</script>");
            }
            var detail = _svc.Value.GetSeller();
            var profile =
                new SellerProfileModel
                {
                    Email = detail.Email,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    PhoneNumber = detail.PhoneNumber,
                    Company = detail.Company,
                    Earning = detail.Earning
                };
            return View("EditSellerProfile", profile);
        }

        public ActionResult EditCustomer()
        {
            if ((bool)Session["isSeller"])
            {
                Response.Write("<script language='javascript'>window.alert('You need to change to a Customer Mode to access this page!'); window.location='../Manage/Index';</script>");
            }
            var detail = _svc.Value.GetCustomer();
            var profile =
                new CustomerProfileModel
                {
                    Email = detail.Email,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    BillingAddress = detail.BillingAddress,
                    ShippingAddress = detail.ShippingAddress,
                    PhoneNumber = detail.PhoneNumber
                };
            return View("EditCustomerProfile", profile);
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName ("EditCustomer")]
        public ActionResult Edit(CustomerProfileModel vm)
        {
            if (!ModelState.IsValid) return View("EditCustomerProfile", vm);

            try
            {
                _svc.Value.UpdateAccount(vm);
            }

            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message + "\nUnable to update profile");
                return View("EditCustomerProfile", vm);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditSeller")]
        public ActionResult EditSeller(SellerProfileModel vm)
        {
            if (!ModelState.IsValid) return View("EditSellerProfile", vm);

            try
            {
                _svc.Value.UpdateAccount(vm);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                ModelState.AddModelError("Error", "The company name has already existed. Please choose a different one.");
                return View("EditSellerProfile", vm);
            }

            catch (Exception e)
            {
                ModelState.AddModelError("Error", e.Message + "\nUnable to update profile");
                return View("EditSellerProfile", vm);
            }

            return RedirectToAction("Index");
        }
    }
}