using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class ClienteServiceTests
    {
        private AgroVisitContext _context;
        private IClienteService _clienteService;

        [TestInitialize]
        public void Initialize()
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisit");
            var options = builder.Options;

            _context = new AgroVisitContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clientes = new List<Cliente>
                {
                    new Cliente { Id = 1, Cpf = "222.333.555-78", Nome = "Maria do Bairro", DataNascimento = DateTime.Parse("1989-05-30"), Cidade = "Minas Gerais", Estado = "MG", IdEngenheiroAgronomo = 1},
                    new Cliente { Id = 2, Cpf = "000.111.222-16", Nome = "João Cabral", DataNascimento = DateTime.Parse("1951-02-23"), Cidade = "Itabaiana", Estado = "SE", IdEngenheiroAgronomo = 1},
                    new Cliente { Id = 3, Cpf = "999.999.999-36", Nome = "Leucides Camargo", DataNascimento = DateTime.Parse("1968-08-13"), Cidade = "Campo do Brito", Estado = "SE", IdEngenheiroAgronomo = 1},
                };

            _context.AddRange(clientes);
            _context.SaveChanges();

            _clienteService = new ClienteService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _clienteService.Create(new Cliente() { Id = 4, Cpf = "123.456.678-10", Nome = "Nestor Almeida", DataNascimento = DateTime.Parse("1989-05-30"), Cidade = "Minas Gerais", Estado = "MG", IdEngenheiroAgronomo = 1 });
            // Assert
            Assert.AreEqual(4, _clienteService.GetAll().Count());
            var autor = _clienteService.Get(4);
            Assert.AreEqual("Nestor Almeida", autor.Nome);
            Assert.AreEqual(DateTime.Parse("1989-05-30"), autor.DataNascimento);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _clienteService.Delete(2);
            // Assert
            Assert.AreEqual(2, _clienteService.GetAll().Count());
            var cliente = _clienteService.Get(2);
            Assert.AreEqual(null, cliente);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var cliente = _clienteService.Get(3);
            cliente.Nome = "Thiago Fritz";
            cliente.DataNascimento = DateTime.Parse("1998-11-21");
            _clienteService.Edit(cliente);
            //Assert
            cliente = _clienteService.Get(3);
            Assert.IsNotNull(cliente);
            Assert.AreEqual("Thiago Fritz", cliente.Nome);
            Assert.AreEqual(DateTime.Parse("1998-11-21"), cliente.DataNascimento);
        }

        [TestMethod()]
        public void GetTest()
        {
            var cliente = _clienteService.Get(1);
            Assert.IsNotNull(cliente);
            Assert.AreEqual("Maria do Bairro", cliente.Nome);
            Assert.AreEqual(DateTime.Parse("1989-05-30"), cliente.DataNascimento);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaAutor = _clienteService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaAutor, typeof(IEnumerable<Cliente>));
            Assert.IsNotNull(listaAutor);
            Assert.AreEqual(3, listaAutor.Count());
            Assert.AreEqual((uint)1, listaAutor.First().Id);
            Assert.AreEqual("Maria do Bairro", listaAutor.First().Nome);
        }

        [TestMethod()]
        public void GetByNome()
        {
            // Act
            var cliente = _clienteService.GetByNome("Maria do Bairro");
            // Assert
            Assert.IsInstanceOfType(cliente, typeof(IEnumerable<Cliente>));
            Assert.IsNotNull(cliente);
            Assert.AreEqual("Maria do Bairro", cliente.First().Nome);
        }
    }
}