using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTests
{
    [TestClass()]
    public class IntervencaoServiceTests
    {
        private AgroVisitContext _context;
        private IIntervencaoService _intervencaoService;

        [TestInitialize]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisitDatabase");
            var options = builder.Options;

            _context = new AgroVisitContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var intervencoes = new List<Intervencao>
            {
                new Intervencao { Id = 1, Pratica = "Realizar poda", Descricao = "Realizar poda nas plantações", IdProjeto = 1 },
                new Intervencao { Id = 2, Pratica = "Realizar irrigação", Descricao = "Realização da irrigação na plantação", IdProjeto = 1 },
                new Intervencao { Id = 3, Pratica = "Aplicação de fertilizante", Descricao = "Realizar aplicação de fertilizante nas plantações", IdProjeto = 1 },
            };

            var projetos = new List<Projeto>
            {
                new Projeto { Id = 1, Nome = "Projeto 1", Descricao = "Descricao do projeto 1" },
                new Projeto { Id = 2, Nome = "Projeto 2", Descricao = "Descricao do projeto 2" },
                new Projeto { Id = 3, Nome = "Projeto 3", Descricao = "Descricao do projeto 3" },
            };

            _context.AddRange(intervencoes);
            _context.AddRange(projetos);
            _context.SaveChanges();

            _intervencaoService = new IntervencaoService(_context);
        }

        [TestMethod()]
        public void IntervencaoServiceTest()
        {
            Assert.IsNotNull(_intervencaoService);
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            await _intervencaoService.Create(new Intervencao
            {
                Id = 4,
                Pratica = "Aplicação de adubo",
                Descricao = "realizar adubação em toda a plantação",
                IdProjeto = 2
            });

            var intervencoes = await _intervencaoService.GetAll();
            Assert.AreEqual(4, intervencoes.Count());

            var intervencao = await _intervencaoService.Get(4);
            Assert.AreEqual("Aplicação de adubo", intervencao.Pratica);
            Assert.AreEqual("realizar adubação em toda a plantação", intervencao.Descricao);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            await _intervencaoService.Delete(2);

            var intervencoes = await _intervencaoService.GetAll();
            Assert.AreEqual(2, intervencoes.Count());

            var intervencao = await _intervencaoService.Get(2);
            Assert.IsNull(intervencao);
        }

        [TestMethod()]
        public async Task EditTest()
        {
            var intervencao = await _intervencaoService.Get(3);
            intervencao.Pratica = "Aplicação de fertilizante NOVO";
            intervencao.Descricao = "Realizar aplicação de fertilizante nas plantações NOVO";

            await _intervencaoService.Edit(intervencao);

            var intervencaoAtualizada = await _intervencaoService.Get(3);
            Assert.IsNotNull(intervencaoAtualizada);
            Assert.AreEqual("Aplicação de fertilizante NOVO", intervencaoAtualizada.Pratica);
            Assert.AreEqual("Realizar aplicação de fertilizante nas plantações NOVO", intervencaoAtualizada.Descricao);
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var intervencao = await _intervencaoService.Get(1);
            Assert.IsNotNull(intervencao);
            Assert.AreEqual("Realizar poda", intervencao.Pratica);
            Assert.AreEqual("Realizar poda nas plantações", intervencao.Descricao);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var listaIntervencao = await _intervencaoService.GetAll();
            Assert.IsInstanceOfType(listaIntervencao, typeof(IEnumerable<Intervencao>));
            Assert.IsNotNull(listaIntervencao);
            Assert.AreEqual(3, listaIntervencao.Count());

            var primeiro = listaIntervencao.First();
            Assert.AreEqual(1, primeiro.Id);
            Assert.AreEqual("Realizar poda", primeiro.Pratica);
        }

        [TestMethod()]
        public async Task GetByProjetoTest()
        {
            var intervencoes = await _intervencaoService.GetByProjeto(1);
            Assert.IsNotNull(intervencoes);
            Assert.AreEqual(3, intervencoes.Count());
            Assert.AreEqual("Realizar poda", intervencoes.First().Pratica);
        }
    }
}
