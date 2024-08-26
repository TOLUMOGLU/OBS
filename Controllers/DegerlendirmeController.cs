using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class DegerlendirmeController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Sinav.ToList();
            return View(list);
        }
        //public ActionResult GetOgrenciDetaylari(int sinavID)
        //{
        //    using (db) // YourDbContext, uygulamanızın DbContext sınıfını temsil etmelidir
        //    {
        //        var ogrenciDetaylar = db.Ogrenci
        //            .Where(od => od.SinavID == sinavID)
        //            .ToList();

        //        return Json(ogrenciDetaylar, JsonRequestBehavior.AllowGet);
        //    }
        //}





    }
}