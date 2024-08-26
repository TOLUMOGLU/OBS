using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OgrenciBilgiSistemi.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        obsEntities db = new obsEntities();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
        }
        [HttpPost]
        public ActionResult Login(Kullanici p)
        {
            var bilgiler = db.Kullanici.FirstOrDefault(x => x.Parola == p.Parola);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                Session["KullaniciAdi"] = bilgiler.KullaniciAdi.ToString();
                Session["Parola"] = bilgiler.Parola.ToString();
                Session["KulllaniciTuru"] = bilgiler.KullaniciTuru.ToString();

                // Kullanıcı rolüne göre yönlendirme
                if (bilgiler.KullaniciTuru == "Öğrenci")

                {
                    

                        var ogrenci = db.Ogrenci.FirstOrDefault(o => o.OgrenciNo == p.KullaniciAdi);
                    if (ogrenci != null)
                    {
                        Session["OgrenciNo"] = ogrenci.OgrenciNo.ToString();
                        return RedirectToAction("IndexOgrenci", "AnaSayfa");
                    }
                    else
                    {

                        // Öğrenci bulunamadıysa hata mesajını ayarla
                        ViewBag.hata = "Öğrenci bulunamadı.";
                        return View(); // View'a geri dön
                    }
                }
                else if (bilgiler.KullaniciTuru == "Öğretim Elemanı")
                {

                    var ogretimelemani = db.OgretimElemani.FirstOrDefault(o => o.KurumSicilNo == p.KullaniciAdi);
                    if (ogretimelemani != null)
                    {
                        Session["KurumSicilNo"] = ogretimelemani.KurumSicilNo.ToString();
                        return RedirectToAction("IndexOgretimElemani", "AnaSayfa");
                    }
                    else
                    {
                        // Öğretim elemanı bulunamadıysa hata mesajını ayarla
                        ViewBag.hata = "Öğretim elemanı bulunamadı.";
                        return View(); // View'a geri dön
                    }
                }
            }
            else
            {
                ViewBag.hata = "Kullanıcı adı veya şifre hatalı";
            }
            return View();
        }




    }
}