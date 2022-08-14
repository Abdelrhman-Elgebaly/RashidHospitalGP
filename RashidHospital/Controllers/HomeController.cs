using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.DAL;
using Microsoft.AspNet.Identity;
using RashidHospital.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RashidHospital.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var user = UserManager.FindById(userId);

            ChemoTherapyCycleDayVM _Obj = new ChemoTherapyCycleDayVM();
            List<ChemoTherapyCycleDayVM> _List = null;

            _List = _Obj.SelectAllPending().Where(a => a.Date == DateTime.Now && a.DoctorId == userId).ToList();
            ViewBag.count = _List.Count();
            if (user.IsActive == false)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                ModelState.AddModelError("", "Invalid login attempt.");
                return RedirectToAction("Login", "Account");
            }
            else
            {
                UserManager.UpdateSecurityStampAsync(userId);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}