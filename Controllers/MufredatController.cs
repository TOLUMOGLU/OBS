using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class MufredatController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Mufredat.ToList();
            return View(list);
        }
        public ActionResult IndexOgrenci()
        {
            var list = db.Mufredat.ToList();
            return View(list);
        }
    }
}