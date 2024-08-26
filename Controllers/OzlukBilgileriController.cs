using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class OzlukBilgileriController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.Ogrenci.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.OgretimElemani.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult DuzenleOzlukOgretimElemani(string id)
        {
            var ogretimElemani = db.OgretimElemani.SingleOrDefault(o => o.KurumSicilNo == id);

            // Ardından ilgili öğretim elemanını view'e geçirin.
            return View(ogretimElemani);
        }

        [HttpPost]
        public ActionResult DuzenleOzlukOgretimElemani(string id, OgretimElemani ogretimElemani)
        {
            var ogrenciToUpdate = db.OgretimElemani.SingleOrDefault(o => o.KurumSicilNo == id);

            if (ogrenciToUpdate != null && ogretimElemani != null)
            {
                ogrenciToUpdate.Adi = ogretimElemani.Adi ?? ogrenciToUpdate.Adi;
                ogrenciToUpdate.Soyadi = ogretimElemani.Soyadi ?? ogrenciToUpdate.Soyadi;
                if (ogretimElemani.Bolum != null && ogretimElemani.Bolum.BolumAdi != null)
                {
                    ogrenciToUpdate.Bolum.BolumAdi = ogretimElemani.Bolum.BolumAdi;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }

            return RedirectToAction("DuzenleOzlukOgretimElemani");
        }


    }
}