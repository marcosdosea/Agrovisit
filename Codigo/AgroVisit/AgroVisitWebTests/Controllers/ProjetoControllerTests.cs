using AgroVisitWeb.Mappers;
using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
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
            var cliente = new Mock<IClienteService>();

            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new ProjetoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAllDto()).ReturnsAsync(GetTestProjetos());
            mockService.Setup(service => service.Get(1)).ReturnsAsync(GetTargetProjetos());
            mockService.Setup(service => service.Edit(It.IsAny<Projeto>())).Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Projeto>())).Verifiable();

            propriedade.Setup(service => service.GetAll()).ReturnsAsync(GetTestsPropriedades());
            propriedade.Setup(service => service.Get(1)).ReturnsAsync(GetNewPropriedade());
            propriedade.Setup(service => service.Create(It.IsAny<Propriedade>())).Verifiable();

            cliente.Setup(service => service.GetAll()).ReturnsAsync(GetTestsCliente());
            cliente.Setup(service => service.Get(1)).ReturnsAsync(GetNewCliente());
            cliente.Setup(service => service.Create(It.IsAny<Cliente>())).Verifiable();

            controller = new ProjetoController(mockService.Object, intervencao.Object, propriedade.Object, cliente.Object, mapper);
        }

        [TestMethod()]
        public async Task IndexTest_Valido()
        {
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<ProjetoDto>));

            IEnumerable<ProjetoDto> lista = (IEnumerable<ProjetoDto>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count());
        }

        [TestMethod()]
        public async Task DetailsTest_Valido()
        {
            // Act
            var result = await controller.Details(1);
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
        }

        [TestMethod()]
        public async Task CreateTest_Get_Valido()
        {
            // Act
            var result = await controller.Create();
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public async Task CreateTest_Valid()
        {
            // Act
            var result = await controller.Create(GetNewProjeto());

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
            var result = await controller.Create(GetNewProjeto());

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
        public async Task EditTest_Post_Valid()
        {
            // Act
            var result = await controller.Edit(GetTargetProjetoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task DeleteTest_Post_Valid()
        {
            // Act
            var result = await controller.Delete(1);
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
        public async Task DeleteTest_Get_Valid()
        {
            // Act
            var result = await controller.Delete(GetTargetProjetoModel().Id, GetTargetProjetoModel());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        // Métodos auxiliares para criar dados de teste...

        private ProjetoViewModel GetNewProjeto()
        {
            return new ProjetoViewModel
            {
                Id = 4,
                Nome = "Projeto soja",
                NomeCliente = "Ana",
                NomePropriedade = "Fazenda 4",
                DataInicio = DateTime.Parse("2021-01-20"),
                DataPrevista = DateTime.Parse("2021-02-20"),
                Status = "EX",
                QuantParcela = 5,
                Valor = 100,
                IdPropriedade = 2
            };
        }

        private Projeto GetTargetProjetos()
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

        private static Propriedade GetNewPropriedade()
        {
            return new Propriedade
            {
                Id = 1,
                Nome = "Fazenda Feliz",
                Estado = "SE",
                Cidade = "Itabaiana",
                IdCliente = 1,
                IdEngenheiroAgronomo = 1,
                IdSolo = 1,
                IdCultura = 1
            };
        }

        private IEnumerable<Propriedade> GetTestsPropriedades()
        {
            return new List<Propriedade>
            {
                new Propriedade
                {
                    Id = 1,
                    Nome = "Fazenda Feliz",
                    Estado = "SE",
                    Cidade = "Itabaiana",
                    IdCliente = 1,
                    IdEngenheiroAgronomo = 1,
                    IdSolo = 1,
                    IdCultura = 1
                },
                new Propriedade
                {
                    Id = 2,
                    Nome = "Alegria",
                    Estado = "SE",
                    Cidade = "Macambira",
                    IdCliente = 1,
                    IdEngenheiroAgronomo = 1,
                    IdSolo = 1,
                    IdCultura = 1
                }
            };
        }

        private IEnumerable<Cliente> GetTestsCliente()
        {
            return new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nome = "Ana",
                    Cpf = "03992849329",
                    Estado = "SE",
                    Cidade = "Itabaiana",
                    IdEngenheiroAgronomo = 1,
                },
                new Cliente
                {
                    Id = 2,
                    Nome = "Joao",
                    Cpf = "999999999",
                    Estado = "SE",
                    Cidade = "Macambira",
                    IdEngenheiroAgronomo = 1
                }
            };
        }

        private static Cliente GetNewCliente()
        {
            return new Cliente
            {
                Id = 1,
                Nome = "Ana",
                Cpf = "029919238293",
                Estado = "SE",
                Cidade = "Itabaiana",
                IdEngenheiroAgronomo = 1
            };
        }

        private ProjetoViewModel GetTargetProjetoModel()
        {
            return new ProjetoViewModel
            {
                Id = 2,
                Nome = "Projeto de irrigação",
                NomeCliente = "Ana",
                NomePropriedade = "Fazenda luz",
                DataInicio = DateTime.Parse("2020-01-20"),
                DataPrevista = DateTime.Parse("2020-02-20"),
                Status = "EX",
                QuantParcela = 5,
                Valor = 900,
                IdPropriedade = 1
            };
        }

        private IEnumerable<ProjetoDto> GetTestProjetos()
        {
            return new List<ProjetoDto>
            {
                new ProjetoDto
                {
                    Id = 1,
                    Nome = "Projeto adubação",
                    DataInicio = DateTime.Parse("2020-10-20"),
                    NomeCliente = "Maria",
                    Status = "EX",
                    Valor = 1900,
                    NomePropriedade = "Fazenda Feliz"
                },
                new ProjetoDto
                {
                    Id = 2,
                    Nome = "Projeto de irrigação",
                    DataInicio = DateTime.Parse("2020-01-20"),
                    Status = "EX",
                    NomeCliente = "João",
                    Valor = 900,
                    NomePropriedade = "Fazenda luz"
                },
                new ProjetoDto
                {
                    Id = 3,
                    Nome = "Projeto milho",
                    DataInicio = DateTime.Parse("2021-10-20"),
                    NomeCliente = "Jose",
                    Status = "EX",
                    Valor = 500,
                    NomePropriedade = "Fazenda"
                },
            };
        }
    }
}
