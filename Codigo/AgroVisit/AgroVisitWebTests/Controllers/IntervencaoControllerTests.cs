using AgroVisitWeb.Mappers;
using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgroVisitWeb.Controllers.Tests
{

    [TestClass()]
    public class IntervencaoControllerTests
    {
        private static IntervencaoController controller;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IIntervencaoService>();
            var projeto = new Mock<IProjetoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new IntervencaoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestIntervencoes());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetIntervencao());
            mockService.Setup(service => service.Edit(It.IsAny<Intervencao>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Intervencao>()))
                .Verifiable();
            controller = new IntervencaoController(mockService.Object, projeto.Object, mapper);
        }

     
        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel intervencaoModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Pratica 1", intervencaoModel.Pratica);
            Assert.AreEqual("Descricao da pratica 1", intervencaoModel.Descricao);
        }

        [TestMethod()]
        public void CreateTest_Get_Valido()
        {
            // Act
            var result = controller.Create();
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreateTest_Valid()
        {
            // Act
            var result = controller.Create(GetNewIntervencao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
        [TestMethod()]
        public void CreateTest_Post_Invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = controller.Create(GetNewIntervencao());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Get_Valid()
        {
            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel autorModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Pratica 1", autorModel.Pratica);
            Assert.AreEqual("Descricao da pratica 1", autorModel.Descricao);
        }

        [TestMethod()]
        public void EditTest_Post_Valid()
        {
            // Act
            var result = controller.Edit(GetTargetIntervencaoModel().Id, GetTargetIntervencaoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void DeleteTest_Post_Valid()
        {
            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel intervencaoModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Pratica 1", intervencaoModel.Pratica);
            Assert.AreEqual("Descricao da pratica 1", intervencaoModel.Descricao);
        }

        [TestMethod()]
        public void DeleteTest_Get_Valid()
        {
            // Act
            var result = controller.Delete(GetTargetIntervencaoModel().Id, GetTargetIntervencaoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        private IntervencaoViewModel GetNewIntervencao()
        {
            return new IntervencaoViewModel
            {
                Id = 4,
                Pratica = "Pratica 4",
                Descricao = "Descricao da pratica 1"
            };

        }
        private static Intervencao GetTargetIntervencao()
        {
            return new Intervencao
            {
                Id = 1,
                Pratica = "Pratica 1",
                Descricao = "Descricao da pratica 1"
            };
        }

        private IntervencaoViewModel GetTargetIntervencaoModel()
        {
            return new IntervencaoViewModel
            {
                Id = 2,
                Pratica = "Pratica 1",
                Descricao = "Descricao da pratica 1"
            };
        }

        private IEnumerable<Intervencao> GetTestIntervencoes()
        {
            return new List<Intervencao>
            {
                new Intervencao
                {
                    Id = 1,
                    Pratica = "Pratica 2",
                    Descricao = "Descricao da pratica 2"
                },
                new Intervencao
                {
                    Id = 2,
                    Pratica = "Pratica 1",
                    Descricao = "Descricao da pratica 1"
                },
                new Intervencao
                {
                    Id = 3,
                    Pratica = "Pratica 3",
                    Descricao = "Descricao da pratica 3"
                },
            };
        }
    }
}
    
