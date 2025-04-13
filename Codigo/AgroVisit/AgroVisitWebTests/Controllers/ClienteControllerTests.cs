using AgroVisitWeb.Controllers;
using AgroVisitWeb.Mappers;
using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

[TestClass()]
public class ClienteControllerTests
{
    private static ClienteController controller;

    [TestInitialize]
    public void Initialize()
    {
        // Arrange
        var mockService = new Mock<IClienteService>();
        var mockPropService = new Mock<IPropriedadeService>();

        IMapper mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new ClienteProfile())).CreateMapper();

        mockService.Setup(service => service.GetAll()).ReturnsAsync(GetTestClientes());

        mockService.Setup(service => service.Get(1)).ReturnsAsync(GetTargetCliente());

        mockService.Setup(service => service.Edit(It.IsAny<Cliente>())).Returns(Task.CompletedTask);

        mockService.Setup(service => service.Create(It.IsAny<Cliente>())).Returns((Task<uint>)Task.CompletedTask);

        controller = new ClienteController(mockService.Object, mockPropService.Object, mapper);
    }

    [TestMethod()]
    public async Task IndexTest_Valido()
    {
        var result = await controller.Index();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<ClienteViewModel>));

        List<ClienteViewModel>? lista = (List<ClienteViewModel>)viewResult.ViewData.Model;
        Assert.AreEqual(3, lista.Count);
    }

    [TestMethod()]
    public async Task DetailsTest_Valido()
    {
        var result = await controller.Details(1);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
        ViewResult viewResult = (ViewResult)result;
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ClienteViewModel));
        ClienteViewModel clienteModel = (ClienteViewModel)viewResult.ViewData.Model;
        Assert.AreEqual("Maria do Bairro", clienteModel.Nome);
        Assert.AreEqual(DateTime.Parse("1989-05-30"), clienteModel.DataNascimento);
    }

    [TestMethod()]
    public async Task CreateTest_Get_Valido()
    {
        var result = controller.Create();

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod()]
    public async Task CreateTest_Valid()
    {
        var result = await controller.Create(GetNewCliente());

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.IsNull(redirectToActionResult.ControllerName);
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }

    [TestMethod()]
    public async Task CreateTest_Post_Invalid()
    {
        controller.ModelState.AddModelError("Nome", "Campo requerido");

        var result = await controller.Create(GetNewCliente());

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
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ClienteViewModel));
        ClienteViewModel clienteModel = (ClienteViewModel)viewResult.ViewData.Model;
        Assert.AreEqual("Maria do Bairro", clienteModel.Nome);
        Assert.AreEqual(DateTime.Parse("1989-05-30"), clienteModel.DataNascimento);
    }

    [TestMethod()]
    public async Task EditTest_Post_Valid()
    {
        var result = await controller.Edit(GetTargetClienteModel());

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
        Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ClienteViewModel));
        ClienteViewModel clienteModel = (ClienteViewModel)viewResult.ViewData.Model;
        Assert.AreEqual("Maria do Bairro", clienteModel.Nome);
        Assert.AreEqual(DateTime.Parse("1989-05-30"), clienteModel.DataNascimento);
    }

    [TestMethod()]
    public async Task DeleteTest_Get_Valid()
    {
        var result = await controller.Delete(GetTargetClienteModel().Id, GetTargetClienteModel());

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
        Assert.IsNull(redirectToActionResult.ControllerName);
        Assert.AreEqual("Index", redirectToActionResult.ActionName);
    }

    private ClienteViewModel GetNewCliente()
    {
        return new ClienteViewModel
        {
            Id = 4,
            Cpf = "000.111.222-16",
            Nome = "João Cabral",
            DataNascimento = DateTime.Parse("1951-02-23"),
            Cidade = "Itabaiana",
            Estado = "SE",
            IdEngenheiroAgronomo = 1
        };
    }

    private static Cliente GetTargetCliente()
    {
        return new Cliente
        {
            Id = 1,
            Cpf = "222.333.555-78",
            Nome = "Maria do Bairro",
            DataNascimento = DateTime.Parse("1989-05-30"),
            Cidade = "Minas Gerais",
            Estado = "MG",
            IdEngenheiroAgronomo = 1
        };
    }

    private ClienteViewModel GetTargetClienteModel()
    {
        return new ClienteViewModel
        {
            Id = 8,
            Cpf = "999.999.999-36",
            Nome = "Leucides Camargo",
            DataNascimento = DateTime.Parse("1968-08-13"),
            Cidade = "Campo do Brito",
            Estado = "SE",
            IdEngenheiroAgronomo = 1
        };
    }

    private IEnumerable<Cliente> GetTestClientes()
    {
        return new List<Cliente>
        {
            new Cliente
            {
                Id = 4,
                Cpf = "000.111.222-16",
                Nome = "João Cabral",
                DataNascimento = DateTime.Parse("1951-02-23"),
                Cidade = "Itabaiana",
                Estado = "SE",
                IdEngenheiroAgronomo = 1
            },
            new Cliente
            {
                Id = 1,
                Cpf = "222.333.555-78",
                Nome = "Maria do Bairro",
                DataNascimento = DateTime.Parse("1989-05-30"),
                Cidade = "Minas Gerais",
                Estado = "MG",
                IdEngenheiroAgronomo = 1
            },
            new Cliente
            {
                Id = 8,
                Cpf = "999.999.999-36",
                Nome = "Leucides Camargo",
                DataNascimento = DateTime.Parse("1968-08-13"),
                Cidade = "Campo do Brito",
                Estado = "SE",
                IdEngenheiroAgronomo = 1
            },
        };
    }
}
