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
    public class VisitaControllerTests
    {
        private static VisitaController controller;

        [TestInitialize]
        public void Initialize()
        {
            var mockService = new Mock<IVisitaService>();
            var mockPropriedade = new Mock<IPropriedadeService>();
            var mockCliente = new Mock<IClienteService>();
            IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile(new VisitaProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll()).ReturnsAsync(GetTestVisitas());
            mockService.Setup(service => service.Get(1)).ReturnsAsync(GetTargetVisita());
            mockService.Setup(service => service.Edit(It.IsAny<Visita>())).Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Visita>())).Verifiable();

            mockPropriedade.Setup(service => service.GetAll()).ReturnsAsync(GetTestPropriedades());
            mockPropriedade.Setup(service => service.Get(1)).ReturnsAsync(GetNewPropriedade());

            mockCliente.Setup(service => service.Get(1)).ReturnsAsync(GetNewCliente());

            controller = new VisitaController(mockService.Object, mockPropriedade.Object, mockCliente.Object, mapper);
        }

        [TestMethod()]
        public async Task IndexTest_Valido()
        {
            var result = await controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<VisitaViewModel>));

            List<VisitaViewModel>? lista = (List<VisitaViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public async Task DetailsTest_Valido()
        {
            var result = await controller.Details(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public async Task CreateTest_Get_Valido()
        {
            var result = await controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public async Task CreateTest_Valid()
        {
            var result = await controller.Create(GetNewVisita());
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task CreateTest_Post_Invalid()
        {
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            var result = await controller.Create(GetNewVisita());
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task EditTest_Get_Valid()
        {
            var result = await controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public async Task EditTest_Post_Valid()
        {
            var result = await controller.Edit(GetTargetVisitaModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public async Task DeleteTest_Post_Valid()
        {
            var result = await controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public async Task DeleteTest_Get_Valido()
        {
            var result = await controller.Delete(GetTargetVisitaModel().Id, GetTargetVisitaModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        private static Visita GetTargetVisita()
        {
            return new Visita
            {
                Id = 1,
                Observacoes = "Jogar conversa fora",
                DataHora = DateTime.Parse("2023-11-05"),
                Status = "A",
                IdPropriedade = 1
            };
        }

        private IEnumerable<Visita> GetTestVisitas()
        {
            return new List<Visita>
            {
                new() {
                    Id = 1,
                    Observacoes = "Entregar sementes tratadas",
                    DataHora = DateTime.Parse("2023-09-30"),
                    Status = "A",
                    IdPropriedade = 1
                },
                new() {
                    Id = 2,
                    Observacoes = "Verificar como está o andamento do projeto",
                    DataHora = DateTime.Parse("2023-08-05"),
                    Status = "C",
                    IdPropriedade = 1
                },
                new() {
                    Id = 3,
                    Observacoes = "Coletar amostra de solo para análise",
                    DataHora = DateTime.Parse("2023-10-11"),
                    Status = "A",
                    IdPropriedade = 1
                },
            };
        }

        private VisitaViewModel GetNewVisita()
        {
            return new()
            {
                Id = 4,
                Observacoes = "Coletar amostra de solo para análise",
                DataHora = DateTime.Parse("2023-10-05"),
                Status = "A",
                IdPropriedade = 1
            };
        }

        private VisitaViewModel GetTargetVisitaModel()
        {
            return new()
            {
                Id = 2,
                Observacoes = "Verificar como está o andamento do projeto",
                DataHora = DateTime.Parse("2023-08-05"),
                Status = "C",
                IdPropriedade = 1,
                NomeCliente = "Janaina Ferreira",
                NomePropriedade = "Fazenda"
            };
        }

        private Propriedade GetNewPropriedade()
        {
            return new()
            {
                Id = 1,
                Nome = "Fazenda",
                Estado = "SE",
                Cidade = "Itabaiana",
                IdCliente = 1,
                IdEngenheiroAgronomo = 1,
                IdSolo = 1,
                IdCultura = 1
            };
        }

        private IEnumerable<Propriedade> GetTestPropriedades()
        {
            return new List<Propriedade>
            {
                new() {
                    Id = 1,
                    Nome = "Fazenda",
                    Estado = "SE",
                    Cidade = "Itabaiana",
                    IdCliente = 1,
                    IdEngenheiroAgronomo = 1,
                    IdSolo = 1,
                    IdCultura = 1
                },
                new() {
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

        private Cliente GetNewCliente()
        {
            return new()
            {
                Id = 1,
                Nome = "Janaina Ferreira",
                Cpf = "01234567891",
                Estado = "SE",
                Cidade = "Ribeirópolis",
                IdEngenheiroAgronomo = 1
            };
        }
    }
}
