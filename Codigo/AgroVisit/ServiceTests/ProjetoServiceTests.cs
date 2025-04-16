using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTests
{
    [TestClass()]
    public class ProjetoServiceTests
    {
        private AgroVisitContext _context;
        private IProjetoService _projetoService;

        [TestInitialize]
        public async Task Initialize()
        {
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisit");
            var options = builder.Options;

            _context = new AgroVisitContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.AddRange(new List<Projeto>
            {
                new Projeto { Id = 1, Nome = "Projeto adubação", DataInicio = DateTime.Parse("1917-12-31"), DataPrevista = DateTime.Parse("1918-01-30"), Valor = 1900, Status = "EX", QuantParcela = 5, IdPropriedade = 1 },
                new Projeto { Id = 2, Nome = "Projeto de irrigação", DataInicio = DateTime.Parse("2000-12-31"), DataPrevista = DateTime.Parse("2001-01-20"), Valor = 900, Status = "EX", QuantParcela = 5, IdPropriedade = 1 },
                new Projeto { Id = 3, Nome = "Projeto milho", DataInicio = DateTime.Parse("1980-12-31"), DataPrevista = DateTime.Parse("1981-01-20"), Valor = 500, Status = "EX", QuantParcela = 5, IdPropriedade = 2 },
            });

            _context.AddRange(new List<Propriedade>
            {
                new Propriedade { Id = 1, Nome = "Fazenda alegria", Estado = "SE", Cidade = "Itabaiana", IdCliente = 1, IdCultura = 1, IdSolo = 1, IdEngenheiroAgronomo = 1 },
                new Propriedade { Id = 2, Nome = "Fazenda tistreza", Estado = "SE", Cidade = "Campo do Brito", IdCliente = 1, IdCultura = 1, IdSolo = 1, IdEngenheiroAgronomo = 1 }
            });

            _context.AddRange(new List<Cliente>
            {
                new Cliente { Id = 1, Nome = "Josimar", Cpf = "00000000", Cidade = "Itabaiana", Estado = "SE", IdEngenheiroAgronomo = 1 }
            });

            _context.AddRange(new List<Intervencao>
            {
                new Intervencao { Id = 1, Pratica = "Aplicar super fosfato", Status = "A", IdProjeto = 1 }
            });

            _context.SaveChanges();
            _projetoService = new ProjetoService(_context);
        }

        [TestMethod]
        public async Task CreateTest()
        {
            await _projetoService.Create(new Projeto
            {
                Id = 4,
                Nome = "Projeto soja",
                Valor = 1000,
                Status = "EX",
                DataInicio = DateTime.Parse("2020-01-20"),
                DataPrevista = DateTime.Parse("2020-02-20"),
                QuantParcela = 5,
                IdPropriedade = 2
            });

            var projetos = await _projetoService.GetAll();
            Assert.AreEqual(4, projetos.Count());

            var projeto = await _projetoService.Get(4);
            Assert.AreEqual("Projeto soja", projeto?.Nome);
        }

        [TestMethod]
        public async Task DeleteTest()
        {
            await _projetoService.Delete(2);
            var projetos = await _projetoService.GetAll();
            Assert.AreEqual(2, projetos.Count());

            var projeto = await _projetoService.Get(2);
            Assert.IsNull(projeto);
        }

        [TestMethod]
        public async Task EditTest()
        {
            var projeto = await _projetoService.Get(3);
            Assert.IsNotNull(projeto);

            projeto.Nome = "Projeto de irrigação";
            await _projetoService.Edit(projeto);

            var projetoAtualizado = await _projetoService.Get(3);
            Assert.AreEqual("Projeto de irrigação", projetoAtualizado?.Nome);
        }

        [TestMethod]
        public async Task GetTest()
        {
            var projeto = await _projetoService.Get(1);
            Assert.IsNotNull(projeto);
            Assert.AreEqual("Projeto adubação", projeto.Nome);
        }

        [TestMethod]
        public async Task GetByNomeTest()
        {
            var projetos = await _projetoService.GetByNome("Projeto adubação");
            Assert.AreEqual(1, projetos.Count());
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var listaProjeto = await _projetoService.GetAllDto();
            Assert.IsInstanceOfType(listaProjeto, typeof(IEnumerable<ProjetoDto>));
            Assert.AreEqual(3, listaProjeto.Count());
        }

        [TestMethod]
        public async Task GetByDataTest()
        {
            var projetos = await _projetoService.GetByData(DateTime.Parse("1918-01-30"));
            Assert.AreEqual(1, projetos.Count());
        }

        [TestMethod]
        public async Task GetByStatusTest()
        {
            var projetos = await _projetoService.GetByStatus("EX");
            Assert.AreEqual(3, projetos.Count());
        }

        [TestMethod]
        public async Task GetAllIntervencoesTest()
        {
            var intervencoes = await _projetoService.GetAllIntervencoes(1);
            Assert.AreEqual(1, intervencoes.Count());
        }

        [TestMethod]
        public async Task GetByPropriedadeTest()
        {
            var projetos = await _projetoService.GetByPropriedade(1);
            Assert.AreEqual(2, projetos.Count());
        }
    }
}
