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
    public class TranskriptController : Controller
    {
        // GET: Mufredat
        obsEntities db = new obsEntities();
        public ActionResult IndexOgrenci()
        {
            var list = db.Mufredat.ToList();
            return View(list);
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
        public class InputDataModel
        {
            public string InputData { get; set; }
        }

    }

}