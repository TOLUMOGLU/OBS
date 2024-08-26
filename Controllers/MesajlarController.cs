using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OgrenciBilgiSistemi.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult Index()
        {
            var list = db.Ogrenci.ToList();
            return View(list);
        }
    }
}