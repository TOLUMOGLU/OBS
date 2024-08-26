using OgrenciBilgiSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;
using System.Data.Entity.Core;
using System.IO;
using ClosedXML.Excel;
using System.Text;






namespace OgrenciBilgiSistemi.Controllers
{
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        obsEntities db = new obsEntities();
        [Authorize]
        public ActionResult IndexOgrenci()
        {
            var list = db.Ogrenci.ToList();
            return View(list);
        }
        public ActionResult IndexOgretimElemani()
        {
            var list = db.Ogrenci.ToList();
            return View(list);
        }
        public ActionResult GoruntuleOgretimElemani(int id)
        {
            // Metot içinde id parametresini kullanarak gerekli işlemleri gerçekleştirin.
            // Örneğin, ilgili öğretim elemanını veritabanından çekme işlemi yapabilirsiniz.
            var ogrenci = db.Ogrenci.SingleOrDefault(o => o.OgrenciID == id);

            // Ardından ilgili öğretim elemanını view'e geçirin.
            return View(ogrenci);
        }


        [HttpPost]
        public ActionResult GoruntuleOgretimElemani(int? id, Ogrenci model)
        {
            if (id.HasValue)
            {
                // id değeri mevcutsa, gelen id'ye göre öğrenci bilgilerini çek
                var ogrenci = db.Ogrenci.SingleOrDefault(o => o.OgrenciID == id.Value);

                if (ogrenci == null)
                {
                    return HttpNotFound();
                }

                return View(ogrenci);
            }
            else
            {
                // id null ise, model içindeki OgrenciID'yi kullanarak işlemi gerçekleştir
                var ogrenci = db.Ogrenci.SingleOrDefault(o => o.OgrenciID == model.OgrenciID);

                if (ogrenci == null)
                {
                    return HttpNotFound();
                }

                return View(ogrenci);
            }
        }


        [HttpGet]
        public ActionResult DuzenleOgretimElemani(int id)
        {
            var ogrenci = db.Ogrenci.SingleOrDefault(o => o.OgrenciID == id);

            // Ardından ilgili öğretim elemanını view'e geçirin.
            return View(ogrenci);
        }



      



        [HttpPost]
        public ActionResult DuzenleOgretimElemani(int id, Ogrenci ogrenci)
        {
            var ogrenciToUpdate = db.Ogrenci.SingleOrDefault(o => o.OgrenciID == id);

            if (ogrenciToUpdate != null && ogrenci != null)
            {
                ogrenciToUpdate.Adi = ogrenci.Adi ?? ogrenciToUpdate.Adi;
                ogrenciToUpdate.Soyadi = ogrenci.Soyadi ?? ogrenciToUpdate.Soyadi;
                ogrenciToUpdate.OgrenciNo = ogrenci.OgrenciNo ?? ogrenciToUpdate.OgrenciNo;
                ogrenciToUpdate.Durumu = ogrenci.Durumu ?? ogrenciToUpdate.Durumu;

                if (ogrenci.Bolum != null && ogrenci.Bolum.BolumAdi != null)
                {
                    ogrenciToUpdate.Bolum.BolumAdi = ogrenci.Bolum.BolumAdi;
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
                            Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }

            return RedirectToAction("DuzenleOgretimElemani");
        }

        public ActionResult SilOgretimElemani(int id)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    // OgrenciID'ye bağlı Degerlendirme kayıtlarını bul
                    var degerlendirmeler = db.Degerlendirme.Where(d => d.OgrenciID == id).ToList();
                    // Degerlendirme kayıtlarını sil
                    db.Degerlendirme.RemoveRange(degerlendirmeler);

                    // OgrenciID'ye bağlı DersAlma kayıtlarını bul
                    var dersAlmalar = db.DersAlma.Where(da => da.OgrenciID == id).ToList();
                    // DersAlma kayıtlarını sil
                    db.DersAlma.RemoveRange(dersAlmalar);

                    // OgrenciID'ye bağlı Danismanlik kayıtlarını bul
                    var danismanliklar = db.Danismanlik.Where(d => d.OgrenciID == id).ToList();
                    // Danismanlik kayıtlarını sil
                    db.Danismanlik.RemoveRange(danismanliklar);

                    // Ogrenci kaydını bul
                    var ogrenci = db.Ogrenci.Find(id);
                    if (ogrenci != null)
                    {
                        // Ogrenci kaydını sil
                        db.Ogrenci.Remove(ogrenci);
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


        [HttpGet]
        public ActionResult OgrenciEkle()
        {
            // Öğrenci ekleme sayfasını görüntülemek için kullanılır
            return View();
        }


        [HttpPost]
        public ActionResult OgrenciEkle(Ogrenci model)
        {

            db = new obsEntities();
            try
            {
                //if (ModelState.IsValid)
                //{
                // Model geçerli ise, öğrenciyi veritabanına ekleyebilirsiniz.
                try
                {
                    db.Ogrenci.Add(model);
                    db.SaveChanges();
                }
                catch(EntityException ee)
                {
                    string a = ee.ToString();
                }

                    // Ekleme işlemi başarılı olduysa, bir başka sayfaya yönlendirme yapabilirsiniz.
                    return RedirectToAction("IndexOgretimElemani", "AnaSayfa");
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

        [HttpPost]
        public ActionResult ConvertToPdf(InputDataModel data)
        {
            // Your PDF conversion logic
            var renderer = new HtmlToPdf();
            var pdf = renderer.RenderHtmlAsPdf(data.InputData);

            // Return the PDF file as a result
            var content = pdf.Stream.ToArray();
            var contentType = "application/pdf";
            var fileName = "converted.pdf";
            return new FileContentResult(content, contentType) { FileDownloadName = fileName };
        }



        [HttpPost]
        public ActionResult ConvertToExcel(InputDataModel data)
        {
            // Your Excel conversion logic
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            // Load HTML content into the worksheet
            //using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data.InputData)))
            //{
            //    worksheet.Cell(1, 1).Value = "Example Data"; // Set a header if needed
            //                                                 // Your HTML to Excel conversion logic
            //                                                 // Example: worksheet.Cell(2, 1).Value = new HTMLImporter().Import(stream);
            //}

            // Return the Excel file as a result
            var content = new byte[0];
            using (var ms = new MemoryStream())
            {
                workbook.SaveAs(ms);
                content = ms.ToArray();
            }

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "table.xlsx";
            return new FileContentResult(content, contentType) { FileDownloadName = fileName };
        }


    }
    public class InputDataModel
    {
        public string InputData { get; set; }
    }
}