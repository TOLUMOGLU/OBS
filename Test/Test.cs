using NUnit.Framework;
using OgrenciBilgiSistemi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using OgrenciBilgiSistemi.Models;

namespace OgrenciBilgiSistemi.Test
{
    

    
    [TestClass]
    public class AnaSayfaControllerTests
    {
        [TestMethod]
        public void IndexOgrenci_ShouldReturnViewResult()
        {
            // Arrange
            var controller = new AnaSayfaController();

            // Act
            var result = controller.IndexOgrenci() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GoruntuleOgretimElemani_WithValidId_ShouldReturnViewResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int validId = 1;

            // Act
            var result = controller.GoruntuleOgretimElemani(validId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GoruntuleOgretimElemani_WithInvalidId_ShouldReturnHttpNotFoundResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int invalidId = -1;

            // Act
            var result = controller.GoruntuleOgretimElemani(invalidId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }


        [TestMethod]
        public void DuzenleOgretimElemani_WithValidId_ShouldReturnViewResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int validId = 1;

            // Act
            var result = controller.DuzenleOgretimElemani(validId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DuzenleOgretimElemani_WithInvalidId_ShouldReturnHttpNotFoundResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int invalidId = -1;

            // Act
            var result = controller.DuzenleOgretimElemani(invalidId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }


        [Test]
        public void SilOgretimElemani_WithValidId_ShouldRedirectToIndex()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int validId = 1;

            // Act
            var result = controller.SilOgretimElemani(validId) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("IndexOgretimElemani", result.RouteValues["action"]);
        }

        [Test]
        public void SilOgretimElemani_WithInvalidId_ShouldReturnHttpNotFoundResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            int invalidId = -1;

            // Act
            var result = controller.SilOgretimElemani(invalidId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
        [Test]
        public void OgrenciEkle_Get_ShouldReturnViewResult()
        {
            // Arrange
            var controller = new AnaSayfaController();

            // Act
            var result = controller.OgrenciEkle() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void OgrenciEkle_Post_WithValidModel_ShouldRedirectToIndexOgretimElemani()
        {
            // Arrange
            var controller = new AnaSayfaController();
            var validModel = new Ogrenci(); // Burada uygun bir öğrenci modeli oluşturmalısınız.

            // Act
            var result = controller.OgrenciEkle(validModel) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("IndexOgretimElemani", result.RouteValues["action"]);
        }

        [Test]
        public void ConvertToPdf_ShouldReturnFileContentResult()
        {
            // Arrange
            var controller = new AnaSayfaController();
            var inputData = new InputDataModel(); // Burada uygun bir InputDataModel oluşturmalısınız.

            // Act
            var result = controller.ConvertToPdf(inputData) as FileContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("application/pdf", result.ContentType);
            Assert.AreEqual("converted.pdf", result.FileDownloadName);
        }

    }
}