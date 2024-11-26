using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class IntervencaoServiceTests
    {
        private AgroVisitContext _context;
        private IIntervencaoService _intervencaoService;




        [TestInitialize]
        public void Initialize(DateTime varchar)
        {
            //Arrange
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisitDatabase");
            var options = builder.Options;

            _context = new AgroVisitContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var intervencoes = new List<Intervencao>
                {
                    new Intervencao { Id = 1, Pratica = "Realizar poda", Descricao = "Realizar poda nas plantações", IdProjeto = 1},
                    new Intervencao { Id = 2, Pratica = "Realizar irrigação", Descricao = "Realização da irrigação na plantação",  IdProjeto = 1 },
                    new Intervencao { Id = 3, Pratica = "Aplicação de fertilizante", Descricao = "Realizar aplicação de fertilizante nas plantações",  IdProjeto = 1},
                };


            var projetos = new List<Projeto>
            {
                new Projeto { Id = 1, Nome = "Projeto 1", Descricao = "Descricao do projeto 1"},
                new Projeto { Id = 2, Nome = "Projeto 2", Descricao = "Descricao do projeto 2" },
                new Projeto { Id = 3, Nome = "Projeto 3", Descricao = "Descricao do projeto 3"},
            };


            _context.AddRange(intervencoes);
            _context.SaveChanges();
            _context.AddRange(projetos);
            _context.SaveChanges();



            _intervencaoService = new IntervencaoService(_context);
        }




        [TestMethod()]
        public void IntervencaoServiceTest()
        {
            Assert.Fail();
        }



        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _intervencaoService.Create(new Intervencao() { Id = 4, Pratica = "Aplicação de adubo", Descricao = "realizar adubação em toda a plantação", IdProjeto = 2 });
            // Assert
            Assert.AreEqual(4, _intervencaoService.GetAll().Count());
            var intervencao = _intervencaoService.Get(4);
            Assert.AreEqual("Aplicação de adubo", intervencao.Pratica);
            Assert.AreEqual("realizar adubação em toda a plantação", intervencao.Descricao);


        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _intervencaoService.Delete(2);
            // Assert
            Assert.AreEqual(2, _intervencaoService.GetAll().Count());
            var intervencao = _intervencaoService.Get(2);
            Assert.AreEqual(null, intervencao);
        }

        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var intervencao = _intervencaoService.Get(3);
            intervencao.Pratica = "Aplicação de fertilizante";
            intervencao.Descricao = "Realizar aplicação de fertilizante nas plantações";
            _intervencaoService.Edit(intervencao);
            //Assert
            intervencao = _intervencaoService.Get(3);
            Assert.IsNotNull(intervencao);
            Assert.AreEqual("Aplicação de fertilizante NOVO", intervencao.Pratica);
            Assert.AreEqual("Realizar aplicação de fertilizante nas plantações NOVO", intervencao.Descricao);
        }

        [TestMethod()]
        public void GetTest()
        {
            var intervencao = _intervencaoService.Get(1);
            Assert.IsNotNull(intervencao);
            Assert.AreEqual("Realizar poda", intervencao.Pratica);
            Assert.AreEqual("Realizar poda nas plantações", intervencao.Descricao);

        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaIntervencao = _intervencaoService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaIntervencao, typeof(IEnumerable<Intervencao>));
            Assert.IsNotNull(listaIntervencao);
            Assert.AreEqual(3, listaIntervencao.Count());
            Assert.AreEqual(1, listaIntervencao.First().Id);
            Assert.AreEqual("Realizar poda", listaIntervencao.First().Pratica);
        }

        [TestMethod()]
        public void GetByProjetoTest()
        {
            //Act
            var intervencoes = _intervencaoService.GetByProjeto(1);
            //Assert
            Assert.IsNotNull(intervencoes);
            Assert.AreEqual(3, intervencoes.Count());
            Assert.AreEqual("Realizar poda", intervencoes.First().Pratica);
        }
    }
}