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
    [TestClass]
    public class ProjetoControllerTests
    {
        private static ProjetoController controller;

        [TestInitialize]

        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IProjetoService>();
            var intervencao = new Mock<IIntervencaoService>();
            var propriedade = new Mock<IPropriedadeService>();


            IMapper mapper = new MapperConfiguration(cfg =>
                cfg.AddProfile(new ProjetoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
                .Returns(GetTestProjetos());
            mockService.Setup(service => service.Get(1))
                .Returns(GetTargetProjetos());
            mockService.Setup(service => service.Edit(It.IsAny<Projeto>()))
                .Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Projeto>()))
                .Verifiable();
            controller = new ProjetoController(mockService.Object, intervencao.Object, propriedade.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest_Valido()
        {
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<ProjetoViewModel>));

            List<ProjetoViewModel>? lista = (List<ProjetoViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest_Valido()
        {
            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ProjetoViewModel));
            ProjetoViewModel projetoModel = (ProjetoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Projeto adubação", projetoModel.Nome);
            Assert.AreEqual(DateTime.Parse("2020-10-20"), projetoModel.DataInicio);
            Assert.AreEqual(DateTime.Parse("2020-11-20"), projetoModel.DataPrevista);
            Assert.AreEqual((uint)5, projetoModel.QuantParcela);
            Assert.AreEqual("EX", projetoModel.Status);
            Assert.AreEqual(1900, projetoModel.Valor);
            Assert.AreEqual((uint)1, projetoModel.IdPropriedade);
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
            var result = controller.Create(GetNewProjeto());

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
            var result = controller.Create(GetNewProjeto());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ProjetoViewModel));
            ProjetoViewModel projetoModel = (ProjetoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Projeto adubação", projetoModel.Nome);
            Assert.AreEqual(DateTime.Parse("2020-10-20"), projetoModel.DataInicio);
            Assert.AreEqual(DateTime.Parse("2020-11-20"), projetoModel.DataPrevista);
            Assert.AreEqual("EX", projetoModel.Status);
            Assert.AreEqual((uint)5, projetoModel.QuantParcela);
            Assert.AreEqual(1900, projetoModel.Valor);
            Assert.AreEqual((uint)1, projetoModel.IdPropriedade);
        }

        [TestMethod()]
        public void EditTest_Post_Valid()
        {
            // Act
            var result = controller.Edit(GetTargetProjetoModel().Id, GetTargetProjetoModel());

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
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ProjetoViewModel));
            ProjetoViewModel projetoModel = (ProjetoViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Projeto adubação", projetoModel.Nome);
            Assert.AreEqual(DateTime.Parse("2020-10-20"), projetoModel.DataInicio);
            Assert.AreEqual(DateTime.Parse("2020-11-20"), projetoModel.DataPrevista);
            Assert.AreEqual("EX", projetoModel.Status);
            Assert.AreEqual((uint)5, projetoModel.QuantParcela);
            Assert.AreEqual(1900, projetoModel.Valor);
            Assert.AreEqual((uint)1, projetoModel.IdPropriedade);
        }

        [TestMethod()]
        public void DeleteTest_Get_Valid()
        {
            // Act
            var result = controller.Delete(GetTargetProjetoModel().Id, GetTargetProjetoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
        private ProjetoViewModel GetNewProjeto()
        {
            return new ProjetoViewModel
            {
                Id = 4,
                Nome = "Projeto soja",
                DataInicio = DateTime.Parse("2021-01-20"),
                DataPrevista = DateTime.Parse("2021-02-20"),
                Status = "EX",
                QuantParcela = 5,
                Valor = 100,
                IdPropriedade = 2
            };

        }
        private static Projeto GetTargetProjetos()
        {
            return new Projeto
            {
                Id = 1,
                Nome = "Projeto adubação",
                DataInicio = DateTime.Parse("2020-10-20"),
                DataPrevista = DateTime.Parse("2020-11-20"),
                Status = "EX",
                QuantParcela = 5,
                Valor = 1900,
                IdPropriedade = 1
            };
        }

        private ProjetoViewModel GetTargetProjetoModel()
        {
            return new ProjetoViewModel
            {
                Id = 2,
                Nome = "Projeto de irrigação",
                DataInicio = DateTime.Parse("2020-01-20"),
                DataPrevista = DateTime.Parse("2020-02-20"),
                Status = "EX",
                QuantParcela = 5,
                Valor = 900,
                IdPropriedade = 1
            };
        }

        private IEnumerable<Projeto> GetTestProjetos()
        {
            return new List<Projeto>
            {
                new Projeto
                {
                    Id = 1,
                    Nome = "Projeto adubação",
                    DataInicio = DateTime.Parse("2020-10-20"),
                    DataPrevista = DateTime.Parse("2020-11-20"),
                    Status = "EX",
                    QuantParcela = 5,
                    Valor = 1900,
                    IdPropriedade = 1
                },
                new Projeto
                {
                    Id = 2,
                    Nome = "Projeto de irrigação",
                    DataInicio = DateTime.Parse("2020-01-20"),
                    DataPrevista = DateTime.Parse("2020-02-20"),
                    Status = "EX",
                    QuantParcela = 5,
                    Valor = 900,
                    IdPropriedade = 1
                },
                new Projeto
                {
                    Id = 3,
                    Nome = "Projeto milho",
                    DataInicio = DateTime.Parse("2021-10-20"),
                    DataPrevista = DateTime.Parse("2021-11-20"),
                    Status = "EX",
                    QuantParcela = 5,
                    Valor = 500,
                    IdPropriedade = 2
                },
            };
        }
    }
}