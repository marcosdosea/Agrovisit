using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTests
{
    [TestClass()]
    public class ClienteServiceTests
    {
        private AgroVisitContext _context;
        private IClienteService _clienteService;

        [TestInitialize]
        public void Initialize()
        {
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
        public async Task CreateTest()
        {
            // Act
            await _clienteService.Create(new Cliente
            {
                Id = 4,
                Cpf = "123.456.678-10",
                Nome = "Nestor Almeida",
                DataNascimento = DateTime.Parse("1989-05-30"),
                Cidade = "Minas Gerais",
                Estado = "MG",
                IdEngenheiroAgronomo = 1
            });

            // Assert
            var clientes = await _clienteService.GetAll();
            Assert.AreEqual(4, clientes.Count());

            var autor = await _clienteService.Get(4);
            Assert.AreEqual("Nestor Almeida", autor.Nome);
            Assert.AreEqual(DateTime.Parse("1989-05-30"), autor.DataNascimento);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            // Act
            await _clienteService.Delete(2);

            // Assert
            var clientes = await _clienteService.GetAll();
            Assert.AreEqual(2, clientes.Count());

            var cliente = await _clienteService.Get(2);
            Assert.IsNull(cliente);
        }

        [TestMethod()]
        public async Task EditTest()
        {
            // Act
            var cliente = await _clienteService.Get(3);
            cliente.Nome = "Thiago Fritz";
            cliente.DataNascimento = DateTime.Parse("1998-11-21");

            await _clienteService.Edit(cliente);

            // Assert
            var clienteAtualizado = await _clienteService.Get(3);
            Assert.IsNotNull(clienteAtualizado);
            Assert.AreEqual("Thiago Fritz", clienteAtualizado.Nome);
            Assert.AreEqual(DateTime.Parse("1998-11-21"), clienteAtualizado.DataNascimento);
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var cliente = await _clienteService.Get(1);
            Assert.IsNotNull(cliente);
            Assert.AreEqual("Maria do Bairro", cliente.Nome);
            Assert.AreEqual(DateTime.Parse("1989-05-30"), cliente.DataNascimento);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var listaCliente = await _clienteService.GetAll();

            Assert.IsInstanceOfType(listaCliente, typeof(IEnumerable<Cliente>));
            Assert.IsNotNull(listaCliente);
            Assert.AreEqual(3, listaCliente.Count());

            var primeiro = listaCliente.First();
            Assert.AreEqual((uint)1, primeiro.Id);
            Assert.AreEqual("Maria do Bairro", primeiro.Nome);
        }

        [TestMethod()]
        public async Task GetByNomeTest()
        {
            var cliente = await _clienteService.GetByNome("Maria do Bairro");

            Assert.IsInstanceOfType(cliente, typeof(IEnumerable<Cliente>));
            Assert.IsNotNull(cliente);
            Assert.AreEqual("Maria do Bairro", cliente.First().Nome);
        }
    }
}
