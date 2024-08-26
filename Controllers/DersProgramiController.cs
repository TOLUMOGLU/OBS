using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class DersProgramiController : Controller
    {
        // GET:DeersProgrami
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.DersProgrami.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.DersProgrami.ToList();
            return View(list);
           
        }
    }
}