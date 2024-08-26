using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        obsEntities db = new obsEntities();
        [Authorize]
        public ActionResult Index()
        {
            // Kullanıcı oturum açmamışsa, Login sayfasına yönlendir.
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // Diğer durumlarda ana sayfayı göster.
            return View();
        }
    }
}