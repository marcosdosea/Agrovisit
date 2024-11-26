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

            mockService.Setup(service => service.GetAll()).Returns(GetTestVisitas());
            mockService.Setup(service => service.Get(1)).Returns(GetTargetVisita());
            mockService.Setup(service => service.Edit(It.IsAny<Visita>())).Verifiable();
            mockService.Setup(service => service.Create(It.IsAny<Visita>())).Verifiable();

            mockPropriedade.Setup(service => service.GetAll()).Returns(GetTestPropriedades());
            mockPropriedade.Setup(service => service.Get(1)).Returns(GetNewPropriedade());

            mockCliente.Setup(service => service.Get(1)).Returns(GetNewCliente());

            controller = new VisitaController(mockService.Object, mockPropriedade.Object, mockCliente.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest_Valido()
        {
            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<VisitaViewModel>));

            List<VisitaViewModel>? lista = (List<VisitaViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest_Valido()
        {
            var result = controller.Details(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public void CreateTest_Get_Valido()
        {
            var result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreateTest_Valid()
        {
            var result = controller.Create(GetNewVisita());
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreatTest_Post_Invalid()
        {
            controller.ModelState.AddModelError("Nome", "Campo requerido");

            var result = controller.Create(GetNewVisita());
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Get_Valid()
        {
            var result = controller.Edit(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public void EditTest_Post_Valid()
        {
            var result = controller.Edit(GetTargetVisitaModel().Id, GetTargetVisitaModel());

            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void DeleteTest_Post_Valid()
        {
            var result = controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(VisitaViewModel));
            VisitaViewModel visitaModel = (VisitaViewModel)viewResult.ViewData.Model;
            Assert.AreEqual("Jogar conversa fora", visitaModel.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-11-05"), visitaModel.DataHora);
        }

        [TestMethod()]
        public void DeleteTest_Get_Valido()
        {
            var result = controller.Delete(GetTargetVisitaModel().Id, GetTargetVisitaModel());

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
                new Visita
                {
                    Id = 1,
                    Observacoes = "Entregar sementes tratadas",
                    DataHora = DateTime.Parse("2023-09-30"),
                    Status = "A",
                    IdPropriedade = 1
                },
                new Visita
                {
                    Id = 2,
                    Observacoes = "Verificar como está o andamento do projeto",
                    DataHora = DateTime.Parse("2023-08-05"),
                    Status = "C",
                    IdPropriedade = 1
                },
                new Visita
                {
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
            return new VisitaViewModel
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
            return new VisitaViewModel
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
            return new Propriedade
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
                new Propriedade
                {
                    Id = 1,
                    Nome = "Fazenda",
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

        private Cliente GetNewCliente()
        {
            return new Cliente
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