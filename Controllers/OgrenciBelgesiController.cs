using OgrenciBilgiSistemi.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IronPdf;

namespace OgrenciBilgiSistemi.Controllers
{
    public class OgrenciBelgesiController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();

        public ActionResult IndexOgrenci()
        {
            var list = db.Ogrenci.ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult ConvertToPdf(InputDataModel data)
        {
            // Your PDF conversion logic
            var ironPdfRenderer = new IronPdf.HtmlToPdf();
            var pdf = ironPdfRenderer.RenderHtmlAsPdf(data.InputData);

            // Return the PDF file as a result
            var content = pdf.Stream.ToArray();
            var contentType = "application/pdf";
            var fileName = "converted.pdf";
            return new FileContentResult(content, contentType) { FileDownloadName = fileName };
        }

        public class InputDataModel
        {
            public string InputData { get; set; }
        }
    }
}
