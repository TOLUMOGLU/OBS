using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class SinavTanimlaController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.Sinav.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Sinav.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult DuzenleOgretimElemani(int id)
        {
            var sinav = db.Sinav.SingleOrDefault(o => o.SinavID == id);

            // Ardından ilgili öğretim elemanını view'e geçirin.
            return View(sinav);
        }



        //[HttpPost]
        //public ActionResult DuzenleOgretimElemani(int id, Sinav sinav)
        //{
        //    var ogrenciToUpdate = db.Sinav.SingleOrDefault(o => o.SinavID == id);

        //    if (ogrenciToUpdate != null && sinav != null)
        //    {
        //        ogrenciToUpdate.DersAcma.Mufredat.DersHavuzu.DersKodu = sinav.DersAcma.Mufredat.DersHavuzu.DersKodu ?? ogrenciToUpdate.DersAcma.Mufredat.DersHavuzu.DersKodu;
        //        ogrenciToUpdate.DersAcma.Mufredat.DersHavuzu.DersAdi = sinav.DersAcma.Mufredat.DersHavuzu.DersAdi ?? ogrenciToUpdate.DersAcma.Mufredat.DersHavuzu.DersAdi;
        //        ogrenciToUpdate.SinavTuru = sinav.SinavTuru ?? ogrenciToUpdate.SinavTuru;
        //        ogrenciToUpdate.SinavTarihi = sinav.SinavTarihi  ?? ogrenciToUpdate.SinavTarihi;

        //        if (sinav.DersAcma.Mufredat.Bolum.BolumAdi != null && sinav.DersAcma.Mufredat.Bolum.BolumAdi != null)
        //        {
        //            ogrenciToUpdate.DersAcma.Mufredat.Bolum.BolumAdi = sinav.DersAcma.Mufredat.Bolum.BolumAdi;

        //            try
        //            {
        //                db.SaveChanges();
        //            }
        //            catch (DbEntityValidationException ex)
        //            {
        //                foreach (var validationErrors in ex.EntityValidationErrors)
        //                {
        //                    foreach (var validationError in validationErrors.ValidationErrors)
        //                    {
        //                        Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
        //                    }
        //                }
        //            }
        //        }

        //        return RedirectToAction("DuzenleOgretimElemani");
        //    }
        //}

        public ActionResult SilOgretimElemaniSinav(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    // OgrenciID'ye bağlı Degerlendirme kayıtlarını bul
                    var degerlendirmeler = db.Degerlendirme.Where(d => d.SinavID == id).ToList();
                    // Degerlendirme kayıtlarını sil
                    db.Degerlendirme.RemoveRange(degerlendirmeler);

                    // Ogrenci kaydını bul
                    var sinav = db.Sinav.Find(id);
                    if (sinav != null)
                    {
                        // Ogrenci kaydını sil
                        db.Sinav.Remove(sinav);
                    }
                    else
                    {
                        // Belirtilen ID'ye sahip öğrenci bulunamadı, gerekirse bir hata mesajı ekleyebilirsiniz.
                        ViewBag.ErrorMessage = "Sinav bulunamadı.";
                    }

                    // Değişiklikleri kaydet
                    db.SaveChanges();

                    // Transaction'ı commit et
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    // Hata durumunda transaction'ı geri al
                    dbContextTransaction.Rollback();
                    throw;
                }
            }

            // Eğer bir işlem sonrasında kullanıcıya başka bir sayfa gösterilecekse RedirectToAction kullanabilirsiniz.
            // Örneğin, tüm öğrencilerin listelendiği bir sayfaya yönlendirme yapabilirsiniz.
            return RedirectToAction("IndexOgretimElemani");
        }


        [HttpGet]
        public ActionResult SinavEkle()
        {
            // Öğrenci ekleme sayfasını görüntülemek için kullanılır
            return View();
        }


        [HttpPost]
        public ActionResult SinavEkle(Sinav model)
        {

            db = new obsEntities();
            try
            {
                //if (ModelState.IsValid)
                //{
                // Model geçerli ise, öğrenciyi veritabanına ekleyebilirsiniz.
                try
                {
                    db.Sinav.Add(model);
                    db.SaveChanges();
                }
                catch (EntityException ee)
                {
                    string a = ee.ToString();
                }

                // Ekleme işlemi başarılı olduysa, bir başka sayfaya yönlendirme yapabilirsiniz.
                return RedirectToAction("IndexOgretimElemani", "SinavTanimla");
                //}
                //else
                //{
                //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                //    {
                //        // Log or print the error messages
                //        Console.WriteLine(error.ErrorMessage);
                //    }
                //    // Model geçerli değilse, hata mesajları ile birlikte ekleme sayfasını tekrar göster
                //    return View(model);
                //}
            }
            catch (DbEntityValidationException ex)
            {
                // Veritabanı işlemi sırasında bir hata oluştu
                Debug.WriteLine($"Veritabanı Hatası: {ex.Message}");

                // Eğer özel bir durumu kontrol etmek istiyorsanız, DbUpdateException'dan türemiş alt istisna türlerini kontrol edebilirsiniz.

                // ModelState'ı güncelle ve hata mesajlarını ekleyerek ekleme sayfasını tekrar göster
                ModelState.AddModelError(string.Empty, "Veritabanı işlemi sırasında bir hata oluştu.");
                return View(model);
            }
            catch (Exception ex)
            {
                // Diğer genel hata durumlarını loglama
                Debug.WriteLine($"Genel Hata: {ex.Message}");

                // ModelState'ı güncelle ve hata mesajlarını ekleyerek ekleme sayfasını tekrar göster
                ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                return View(model);
            }
        }

    }
}