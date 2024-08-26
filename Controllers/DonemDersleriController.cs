using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace OgrenciBilgiSistemi.Controllers
{
    public class DonemDersleriController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.Mufredat.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Mufredat.ToList();
            return View(list);
        }
        [HttpPost]
        public ActionResult GeneratePdf()
        {
            // Öğrenci bilgilerini al, bu verileri projenize uygun şekilde düzenleyin.
            // Örnek olarak, bir Öğrenci modeli kullanabilirsiniz.
            var list = db.Mufredat.ToList();
            return View(list);
            // PDF dosyasını oluşturmak için iTextSharp kütüphanesini kullanın.
            var pdfPath = Server.MapPath("~/Content/GeneratedFiles/student_info.pdf");

            using (var writer = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    //document.Add(new Paragraph($"Öğrenci Bilgileri\nAdı: {studentInfo.Ad}\nSoyadı: {studentInfo.Soyad}\nT.C. Kimlik No: {studentInfo.TCKimlikNo}"));
                    // Diğer öğrenci bilgilerini ekleyin...

                    ViewBag.PdfPath = pdfPath; // PDF dosyasının yolunu view'e aktarın.
                }
            }

            return View("Index");
        }
    }
}