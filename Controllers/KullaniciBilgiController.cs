using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class KullaniciBilgiController : Controller
    {
        // GET: KullaniciBilgi
        obsEntities db =new obsEntities();
        [Authorize]

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
        public ActionResult GuncelleOgrenci(string id)
        {
            // KurumSicilNo'yu int'e çevir
            if (int.TryParse(id, out int OgrenciNo))
            {
                // Çevirme başarılıysa sorguyu gerçekleştir
                var guncelle = db.Ogrenci.FirstOrDefault(x => x.OgrenciNo == id);

                // Eğer öğretim elemanı bulunamazsa, hata veya başka bir işlem gerçekleştirebilirsiniz
                if (guncelle == null)
                {
                    return HttpNotFound(); // Örnek: 404 Not Found sayfasına yönlendirme
                }

                return View(guncelle);
            }
            return HttpNotFound();
        }

        public ActionResult GuncelleOgretimElemani(string id)
        {
            // KurumSicilNo'yu int'e çevir
            if (int.TryParse(id, out int kurumSicilNo))
            {
                // Çevirme başarılıysa sorguyu gerçekleştir
                var guncelle = db.OgretimElemani.FirstOrDefault(x => x.KurumSicilNo == id);

                // Eğer öğretim elemanı bulunamazsa, hata veya başka bir işlem gerçekleştirebilirsiniz
                if (guncelle == null)
                {
                    return HttpNotFound(); // Örnek: 404 Not Found sayfasına yönlendirme
                }

                return View(guncelle);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult IndexOgrenci(Ogrenci Data)
        {
            db.Ogrenci.Add(Data);
            return RedirectToAction("IndexOgrenci");
        }
        [HttpPost]
        public ActionResult IndexOgretimElemani(OgretimElemani Data)
        {
            db.OgretimElemani.Add(Data);
            return RedirectToAction("IndexOgretimElemani");
        }
        [HttpPost]
        public ActionResult GuncelleOgrenci(Ogrenci Data)
        {
            var ogrenci = db.Ogrenci.Find(Data.OgrenciNo);
            ogrenci.OgrenciNo = Data.OgrenciNo;
            ogrenci.Durumu = Data.Durumu;
            ogrenci.KayitTarihi = Data.KayitTarihi;
            ogrenci.AyrilmaTarihi = Data.AyrilmaTarihi;
            ogrenci.Adi = Data.Adi;
            ogrenci.Soyadi = Data.Soyadi;
            ogrenci.TCKimlikNo = Data.TCKimlikNo;
            ogrenci.Cinsiyet = Data.Cinsiyet;
            ogrenci.DogumTarihi = Data.DogumTarihi;
            ogrenci.KullaniciID = Data.KullaniciID;
            db.SaveChanges();
            return RedirectToAction("IndexOgrenci");
            
        }
        [HttpPost]
        public ActionResult GuncelleOgretimElemani(OgretimElemani Data)
        {
            db.OgretimElemani.Add(Data);
            return RedirectToAction("GuncelleOgretimElemani");
        }


    }
}