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
    public class MemberController : Controller
    {
        public ApplicationDbContext Context = new ApplicationDbContext();

        
        public ActionResult CheckEmail(string text)
        {
            try
            {
                var result = Context.Users.FirstOrDefault(x => x.Email == text);

                if(result==null)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {
                return Json(2, JsonRequestBehavior.AllowGet);
            }

        }

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

        public ActionResult UserDetails()
        {
            if(Request.Cookies.AllKeys.Contains("ck1") && Decrypt(Request.Cookies["iden"].Value)=="1")
            {
                
                ViewBag.Status = "1";
                var emailAddress = Decrypt(Request.Cookies["ck1"].Value);
                ViewBag.UserName = emailAddress;
                var registration = new Registration();
                registration = Context.Registrations.FirstOrDefault(x => x.Email == emailAddress);

                //yourDateTime.ToString("MMMM dd, yyyy");
                var userRegistration = new RegistrationUserDetails
                {
                    Email = registration.Email,
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Address = registration.Address,
                    Phone = registration.Phone,
                    Birthdate = (registration.Birthdate).ToString("dd-MM-yyyy")
                };

                return View(userRegistration);
            }
            else if(Request.Cookies.AllKeys.Contains("ck1") && Decrypt(Request.Cookies["iden"].Value) == "2")
            {
                ViewBag.Status = "2";
                return RedirectToAction("MembersList", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }


        public ActionResult MemberLayout()
        {
            return View();
        }

    

    }
}