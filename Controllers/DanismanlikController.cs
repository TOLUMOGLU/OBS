using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class DanismanlikController : Controller
    {
        // GET:
        obsEntities db = new obsEntities();
     
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Danismanlik.ToList();
            return View(list);

        }
        public ActionResult Sil(int id)
        {
            var danismanlik = db.Danismanlik.Find(id);

            if (danismanlik != null)
            {
                db.Danismanlik.Remove(danismanlik);
                db.SaveChanges();
            }
            else
            {
                // Belirtilen ID'ye sahip öğrenci bulunamadı, gerekirse bir hata mesajı ekleyebilirsiniz.
                ViewBag.ErrorMessage = "Öğrenci bulunamadı.";
            }

            // Eğer bir işlem sonrasında kullanıcıya başka bir sayfa gösterilecekse RedirectToAction kullanabilirsiniz.
            // Örneğin, tüm öğrencilerin listelendiği bir sayfaya yönlendirme yapabilirsiniz.
            return RedirectToAction("IndexOgretimElemani");
        }



    }
}