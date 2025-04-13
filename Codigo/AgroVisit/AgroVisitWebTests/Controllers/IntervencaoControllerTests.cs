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
                .ReturnsAsync(GetTestIntervencoes());
            mockService.Setup(service => service.Get(1))
                .ReturnsAsync(GetTargetIntervencao());
            mockService.Setup(service => service.Edit(It.IsAny<Intervencao>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Intervencao>()))
                .Verifiable();
            controller = new IntervencaoController(mockService.Object, projeto.Object, mapper);
        }

        [TestMethod()]
        public async Task DetailsTest()
        {
            // Act
            var result = await controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel intervencaoModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Realizar poda", intervencaoModel.Pratica);
            Assert.AreEqual("Realizar poda nas plantações", intervencaoModel.Descricao);
        }

        [TestMethod()]
        public void CreateTest_Get_Valido()
        {
            // Act
            var result = controller.Create(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod()]
        public async Task CreateTest_Valid()
        {
            // Act
            var result = await controller.Create(GetNewIntervencao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task CreateTest_Post_Invalid()
        {
            // Arrange
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            // Act
            var result = await controller.Create(GetNewIntervencao());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task EditTest_Get_Valid()
        {
            // Act
            var result = await controller.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            PartialViewResult viewResult = (PartialViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel autorModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Realizar poda", autorModel.Pratica);
            Assert.AreEqual("Realizar poda nas plantações", autorModel.Descricao);
        }

        [TestMethod()]
        public async Task EditTest_Post_Valid()
        {
            // Act
            var result = await controller.Edit(GetTargetIntervencaoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            JsonResult redirectToActionResult = (JsonResult)result;
            Assert.IsNotNull(JsonResult.ReferenceEquals);
            Assert.AreNotEqual("Details", JsonResult.ReferenceEquals);
        }

        [TestMethod()]
        public async Task DeleteTest_Post_Valid()
        {
            // Act
            var result = await controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IntervencaoViewModel));
            IntervencaoViewModel intervencaoModel = (IntervencaoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Realizar poda", intervencaoModel.Pratica);
            Assert.AreEqual("Realizar poda nas plantações", intervencaoModel.Descricao);
        }

        [TestMethod()]
        public async Task DeleteTest_Get_Valid()
        {
            // Act
            var result = await controller.Delete(GetTargetIntervencaoModel().Id, GetTargetIntervencaoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        }

        private IntervencaoViewModel GetNewIntervencao()
        {
            return new IntervencaoViewModel
            {
                Id = 4,
                Pratica = "Aplicação de adubo",
                Descricao = "Realizar poda nas plantações"
            };
        }

        private static Intervencao GetTargetIntervencao()
        {
            return new Intervencao
            {
                Id = 1,
                Pratica = "Realizar poda",
                Descricao = "Realizar poda nas plantações"
            };
        }

        private IntervencaoViewModel GetTargetIntervencaoModel()
        {
            return new IntervencaoViewModel
            {
                Id = 2,
                Pratica = "Realizar poda",
                Descricao = "Realizar poda nas plantações"
            };
        }

        private IEnumerable<Intervencao> GetTestIntervencoes()
        {
            return new List<Intervencao>
            {
                new Intervencao
                {
                    Id = 1,
                    Pratica = "Realizar irrigação",
                    Descricao = "Realização da irrigação na plantação"
                },
                new Intervencao
                {
                    Id = 2,
                    Pratica = "Realizar poda",
                    Descricao = "Realizar poda nas plantações"
                },
                new Intervencao
                {
                    Id = 3,
                    Pratica = "Aplicação de fertilizante",
                    Descricao = "Realizar aplicação de fertilizante nas plantações"
                },
            };
        }
    }
}
