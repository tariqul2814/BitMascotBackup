using Bit_Mascot.Models;
using Bit_Mascot.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bit_Mascot.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ApplicationDbContext Context = new ApplicationDbContext();

        public ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public static string Encrypt(string plaintextValue)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintextValue);
            return MachineKey.Encode(plaintextBytes, MachineKeyProtection.All);
        }

        public static string Decrypt(string encryptedValue)
        {
            try
            {
                var decryptedBytes = MachineKey.Decode(encryptedValue, MachineKeyProtection.All);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch
            {
                return null;
            }
        }

        public ActionResult MembersList()
        {
            if (Request.Cookies.AllKeys.Contains("ck1") && Decrypt(Request.Cookies["iden"].Value) == "1")
            {
                ViewBag.Status = "1";
                return RedirectToAction("UserDetails", "Member");
            }
            else if (Request.Cookies.AllKeys.Contains("ck1") && Decrypt(Request.Cookies["iden"].Value) == "2")
            {
                ViewBag.Status = "2";
                var emailAddress = Decrypt(Request.Cookies["ck1"].Value);
                ViewBag.UserName = emailAddress;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
                
        }
        
        
        public JsonResult Members(string text)
        {
            text = text.ToUpper();
            List<UserListViewModel> registration = new List<UserListViewModel>();

            registration = /*context.Registrations.ToList();*/ (from r in context.Registrations.ToList()
                                                                where r.Identity==1
                                                                select new UserListViewModel
                                                                {
                                                                    Email = r.Email,
                                                                    FirstName = r.FirstName.ToUpper() + " " + r.LastName.ToUpper(),
                                                                    LastName = "",
                                                                    Birthdate = r.Birthdate.ToString("MM/dd/yyyy"),
                                                                    Phone = r.Phone
                                                                }).ToList();

            if (text.Equals(""))
            {
                
                return Json(new { datas = registration }, JsonRequestBehavior.AllowGet);
            }
            

            else
            {
                List<UserListViewModel> registration1 = new List<UserListViewModel>();
                registration1 = (from r in registration.ToList()
                                 where r.FirstName.Contains(text)
                                 select new UserListViewModel { Email = r.Email, FirstName = r.FirstName.ToUpper(), LastName = "", Birthdate = r.Birthdate, Phone = r.Phone }).ToList();

                return Json(new { datas = registration1 }, JsonRequestBehavior.AllowGet);
            }
            
            
        }
    }
}