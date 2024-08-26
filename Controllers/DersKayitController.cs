using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class DersKayitController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.DersHavuzu.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.DersHavuzu.ToList();
            return View(list);
        }

        public ActionResult CikarOgrenci(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    // OgrenciID'ye bağlı Degerlendirme kayıtlarını bul
                    var mufredatlar = db.Mufredat.Where(d => d.DersID == id).ToList();
                    // Degerlendirme kayıtlarını sil
                    db.Mufredat.RemoveRange(mufredatlar);

                   

                    // Ogrenci kaydını bul
                    var ders = db.DersHavuzu.Find(id);
                    if (ders != null)
                    {
                        // Ogrenci kaydını sil
                        db.DersHavuzu.Remove(ders);
                    }
                    else
                    {
                        // Belirtilen ID'ye sahip öğrenci bulunamadı, gerekirse bir hata mesajı ekleyebilirsiniz.
                        ViewBag.ErrorMessage = "Öğrenci bulunamadı.";
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



        public class DersOnaylamaModel
        {
            public List<DersHavuzu> OgretimElemaniDersListesi { get; set; }
            public List<Ogrenci> IlgiDersiAlanOgrenciler { get; set; }
            // Diğer özellikler eklenebilir

            public DersOnaylamaModel()
            {
                OgretimElemaniDersListesi = new List<DersHavuzu>();
                IlgiDersiAlanOgrenciler = new List<Ogrenci>();
            }
        }


        public ActionResult Index()
        {

            // Öğretim elemanının verdiği ders listesini al
            List<DersHavuzu> ogretimElemaniDersListesi = GetOgretimElemaniDersListesi();

            // İlgili dersi alan öğrenci listesini al
            List<Ogrenci> ilgiDersiAlanOgrenciler = GetIlgiDersiAlanOgrenciler();

            // Modeli oluşturup özelliklere listeleri atayalım
            var model = new DersOnaylamaModel
            {
                OgretimElemaniDersListesi = ogretimElemaniDersListesi,
                IlgiDersiAlanOgrenciler = ilgiDersiAlanOgrenciler
                // Diğer gerekli özellikler buraya eklenebilir
            };
            return View(model);
        }

        private List<DersHavuzu> GetOgretimElemaniDersListesi()
        {
            // TODO: Bu metodun içinde öğretim elemanının verdiği dersleri listeleyen bir sorgu yapılmalı
            // Bu metodun gerçek hayatta veritabanı sorguları içermesi önerilir
            List<DersHavuzu> ogretimElemaniDersListesi = new List<DersHavuzu>();
            // Ders listesini doldur

            return ogretimElemaniDersListesi;
        }

        private List<Ogrenci> GetIlgiDersiAlanOgrenciler()
        {
            // TODO: Bu metodun içinde ilgili dersi alan öğrencileri listeleyen bir sorgu yapılmalı
            // Bu metodun gerçek hayatta veritabanı sorguları içermesi önerilir
            List<Ogrenci> ilgiDersiAlanOgrenciler = new List<Ogrenci>();
            // Öğrenci listesini doldur

            return ilgiDersiAlanOgrenciler;
        }
        //public ActionResult Reddet(int id)
        //{
        //    using (var dbContextTransaction = db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // OgrenciID'ye bağlı Degerlendirme kayıtlarını bul
        //            var degerlendirmeler = db.Degerlendirme.Where(d => d.OgrenciID == id).ToList();
        //            // Degerlendirme kayıtlarını sil
        //            db.Degerlendirme.RemoveRange(degerlendirmeler);

        //            // OgrenciID'ye bağlı DersAlma kayıtlarını bul
        //            var dersAlmalar = db.DersAlma.Where(da => da.OgrenciID == id).ToList();
        //            // DersAlma kayıtlarını sil
        //            db.DersAlma.RemoveRange(dersAlmalar);

        //            // OgrenciID'ye bağlı Danismanlik kayıtlarını bul
        //            var danismanliklar = db.Danismanlik.Where(d => d.OgrenciID == id).ToList();
        //            // Danismanlik kayıtlarını sil
        //            db.Danismanlik.RemoveRange(danismanliklar);

        //            // Ogrenci kaydını bul
        //            var ders = db.DersHavuzu.Find(id);
        //            if (ders != null)
        //            {
        //                // Ogrenci kaydını sil
        //                db.DersHavuzu.Remove(ders);
        //            }
        //            else
        //            {
        //                // Belirtilen ID'ye sahip öğrenci bulunamadı, gerekirse bir hata mesajı ekleyebilirsiniz.
        //                ViewBag.ErrorMessage = "Öğrenci bulunamadı.";
        //            }

        //            // Değişiklikleri kaydet
        //            db.SaveChanges();

        //            // Transaction'ı commit et
        //            dbContextTransaction.Commit();
        //        }
        //        catch (Exception)
        //        {
        //            // Hata durumunda transaction'ı geri al
        //            dbContextTransaction.Rollback();
        //            throw;
        //        }
        //    }

        //    // Eğer bir işlem sonrasında kullanıcıya başka bir sayfa gösterilecekse RedirectToAction kullanabilirsiniz.
        //    // Örneğin, tüm öğrencilerin listelendiği bir sayfaya yönlendirme yapabilirsiniz.
        //    return RedirectToAction("IndexOgretimElemani");
        //}



        [HttpPost]
        public ActionResult PerformAction(int ogrenciId, int dersId, string action)
        {
            // Bu örnekte, işlemi gerçekleştirmek için basit bir öğrenci ve ders listesi kullanılıyor.
            // Gerçek projede, veritabanı veya başka bir kaynak kullanılabilir.

            // Öğrenci ve ders listesi
            List<DersHavuzu> dersListesi = GetOgretimElemaniDersListesi();
            List<Ogrenci> ogrenciListesi = GetIlgiDersiAlanOgrenciler();

            // İlgili öğrenciyi ve dersi bul
            Ogrenci ogrenci = ogrenciListesi.FirstOrDefault(o => o.OgrenciID == ogrenciId);
            DersHavuzu ders = dersListesi.FirstOrDefault(d => d.DersID == dersId);

            if (ogrenci != null && ders != null)
            {
                // İlgili eyleme göre durumu güncelle
                switch (action.ToLower())
                {
                    case "onayla":
                        ogrenci.Durumu = "Onaylandı";
                        break;

                    case "beklet":
                        ogrenci.Durumu = "Bekliyor";
                        break;

                    case "reddet":
                        ogrenci.Durumu = "Reddedildi";
                        break;

                    default:
                        return Json(new { success = false, message = "Geçersiz eylem." });
                }

                // Başarı mesajı ile birlikte güncellenmiş öğrenci listesini döndür
                return Json(new { success = true, message = $"{action} başarıyla gerçekleştirildi.", ogrenciListesi });
            }
            else
            {
                // Öğrenci veya ders bulunamadı
                return Json(new { success = false, message = "Öğrenci veya ders bulunamadı." });
            }
        }

        
    }
}
