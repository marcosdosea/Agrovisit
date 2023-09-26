using Core;
using Core.DTO;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class ProjetoServiceTests
    {
       
        private AgroVisitContext _context;
        private IProjetoService _projetoService;

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
            var projetos = new List<Projeto>
                {
                    new Projeto { Id = 1, Nome = "Projeto adubação",DataInicio = DateTime.Parse("1917-12-31"), DataPrevista = DateTime.Parse("1918-01-30"), Valor =  1900, Status = "EX", QuantParcela = 5, IdPropriedade = 1},
                    new Projeto { Id = 2, Nome = "Projeto de irrigação",DataInicio = DateTime.Parse("2000-12-31"), DataPrevista = DateTime.Parse("2001-01-20"), Valor =  900, Status = "EX", QuantParcela = 5, IdPropriedade = 1},
                    new Projeto { Id = 3, Nome = "Projeto milho", DataInicio = DateTime.Parse("1980-12-31"), DataPrevista = DateTime.Parse("1981-01-20"), Valor =  500, Status = "EX", QuantParcela = 5, IdPropriedade = 2},
                };
            var intervencoes = new List<Intervencao>
                {
                    new Intervencao { Id = 1, Pratica = "Aplicar super fosfato",Status = "A", IdProjeto = 1},
                    
                };

            _context.AddRange(projetos);
            _context.SaveChanges();
            _context.AddRange(intervencoes);
            _context.SaveChanges();
            _projetoService = new ProjetoService(_context);
        }
        [TestMethod()]
        public void ProjetoServiceTest()
        {
            // Act
            _projetoService.Create(new Projeto()
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

            // Assert
            Assert.AreEqual(4, _projetoService.GetAll().Count());
            var projeto = _projetoService.Get(4);
            Assert.AreEqual("Projeto soja", projeto.Nome);
            Assert.AreEqual(1000, projeto.Valor);
            Assert.AreEqual(DateTime.Parse("2020-01-20"), projeto.DataInicio);
            Assert.AreEqual(DateTime.Parse("2020-02-20"), projeto.DataPrevista);
            Assert.AreEqual("EX", projeto.Status);
            Assert.AreEqual((uint)5, projeto.QuantParcela);
            Assert.AreEqual((uint)2, projeto.IdPropriedade);
            
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            _projetoService.Create(new Projeto() { Id = 4, Nome = "Projeto soja", DataInicio = DateTime.Parse("2020-01-20"), DataPrevista = DateTime.Parse("2020-02-20"), Valor = 1000, Status = "EX", QuantParcela = 5, IdPropriedade = 2 });
            // Assert
            Assert.AreEqual(4, _projetoService.GetAll().Count());
            var projeto = _projetoService.Get(4);
            if (projeto != null)
            {
                Assert.AreEqual("Projeto soja", projeto.Nome);
                Assert.AreEqual(DateTime.Parse("2020-01-20"), projeto.DataInicio);
                Assert.AreEqual(DateTime.Parse("2020-02-20"), projeto.DataPrevista);
                Assert.AreEqual("EX", projeto.Status);
                Assert.AreEqual((uint)5, projeto.QuantParcela);
                Assert.AreEqual((uint)2, projeto.IdPropriedade);
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            _projetoService.Delete(2);
            // Assert
            Assert.AreEqual(2, _projetoService.GetAll().Count());
            var projeto = _projetoService.Get(2);
            Assert.AreEqual(null, projeto);
        }
        [TestMethod()]
        public void EditTest()
        {
            //Act 
            var projeto = _projetoService.Get(3);
            if (projeto != null)
            {
                projeto.Nome = "Projeto de irrigação";
                projeto.DataInicio = DateTime.Parse("1980-12-31");
                projeto.DataPrevista = DateTime.Parse("1981-01-20");
                projeto.Status = "EX";
                projeto.QuantParcela = 5;
                projeto.IdPropriedade = 2;

                _projetoService.Edit(projeto);
                //Assert
                projeto = _projetoService.Get(3);
                Assert.IsNotNull(projeto);
                Assert.AreEqual("Projeto de irrigação", projeto.Nome);
                Assert.AreEqual(DateTime.Parse("1980-12-31"), projeto.DataInicio);
                Assert.AreEqual(DateTime.Parse("1981-01-20"), projeto.DataPrevista);
                Assert.AreEqual("EX", projeto.Status);
                Assert.AreEqual((uint)5, projeto.QuantParcela);
                Assert.AreEqual((uint)2, projeto.IdPropriedade);
            }

        }
        [TestMethod()]
        public void GetTest()
        {
            var projeto = _projetoService.Get(1);
            Assert.IsNotNull(projeto);
            Assert.AreEqual("Projeto adubação", projeto.Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), projeto.DataInicio);
            Assert.AreEqual(DateTime.Parse("1918-01-30"), projeto.DataPrevista);
            Assert.AreEqual("EX", projeto.Status);
            Assert.AreEqual((uint)5, projeto.QuantParcela);
            Assert.AreEqual((uint)1, projeto.IdPropriedade);
        }

        [TestMethod()]
        public void GetByNomeTest()
        {
            //Act
            var projetos = _projetoService.GetByNome("Projeto adubação");
            // Assert
            Assert.IsNotNull(projetos);
            Assert.AreEqual(1, projetos.Count());
            Assert.AreEqual((uint)1, projetos.First().Id);
            Assert.AreEqual("Projeto adubação", projetos.First().Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), projetos.First().DataInicio);
            Assert.AreEqual(DateTime.Parse("1918-01-30"), projetos.First().DataPrevista);
            Assert.AreEqual("EX", projetos.First().Status);
            Assert.AreEqual((uint)5, projetos.First().QuantParcela);
            Assert.AreEqual((uint)1, projetos.First().IdPropriedade);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var listaProjeto = _projetoService.GetAll();
            // Assert
            Assert.IsInstanceOfType(listaProjeto, typeof(IEnumerable<ProjetoDTO>));
            Assert.IsNotNull(listaProjeto);
            Assert.AreEqual(3, listaProjeto.Count());
            Assert.AreEqual((uint)1, listaProjeto.First().Id);
            Assert.AreEqual("Projeto adubação", listaProjeto.First().Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), listaProjeto.First().DataInicio);
            Assert.AreEqual("EX", listaProjeto.First().Status);
        }

        [TestMethod()]
        public void GetByDataTest()
        {
            //Act
            var projetos = _projetoService.GetByData(DateTime.Parse("1918-01-30"));
            // Assert
            Assert.IsNotNull(projetos);
            Assert.AreEqual(1, projetos.Count());
            Assert.AreEqual((uint)1, projetos.First().Id);
            Assert.AreEqual("Projeto adubação", projetos.First().Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), projetos.First().DataInicio);
            Assert.AreEqual(DateTime.Parse("1918-01-30"), projetos.First().DataPrevista);
            Assert.AreEqual("EX", projetos.First().Status);
            Assert.AreEqual((uint)5, projetos.First().QuantParcela);
            Assert.AreEqual((uint)1, projetos.First().IdPropriedade);
        }

        [TestMethod()]
        public void GetByStatusTest()
        {
            //Act
            var projetos = _projetoService.GetByStatus("EX");
            // Assert
            Assert.IsNotNull(projetos);
            Assert.AreEqual(3, projetos.Count());
            Assert.AreEqual("Projeto adubação", projetos.First().Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), projetos.First().DataInicio);
            Assert.AreEqual(DateTime.Parse("1918-01-30"), projetos.First().DataPrevista);
            Assert.AreEqual("EX", projetos.First().Status);
            Assert.AreEqual((uint)5, projetos.First().QuantParcela);
            Assert.AreEqual((uint)1, projetos.First().IdPropriedade);
        }

        [TestMethod()]
        public void GetAllIntervencoesTest()
        {
            //Act
            var intervencoes = _projetoService.GetAllIntervencoes(1);
            // Assert
            Assert.IsNotNull(intervencoes);
            Assert.AreEqual(1, intervencoes.Count());
            Assert.AreEqual("Aplicar super fosfato", intervencoes.First().Pratica);
            
        }

        /*[TestMethod()]
        public void GetAllContaTest()
        {
            //Act
            var contas = _projetoService.GetAllConta(1);
            // Assert
            Assert.IsNotNull(contas);
            Assert.AreEqual(1, contas.Count());
            Assert.AreEqual(500, contas.First().Valor);
        }*/

        [TestMethod()]
        public void GetByPropriedadeTest()
        {

            //Act
            var projetos = _projetoService.GetByPropriedade(1);
            // Assert
            Assert.IsNotNull(projetos);
            Assert.AreEqual(2, projetos.Count());
            Assert.AreEqual("Projeto adubação", projetos.First().Nome);
            Assert.AreEqual(DateTime.Parse("1917-12-31"), projetos.First().DataInicio);
            Assert.AreEqual(DateTime.Parse("1918-01-30"), projetos.First().DataPrevista);
            Assert.AreEqual("EX", projetos.First().Status);
            Assert.AreEqual((uint)5, projetos.First().QuantParcela);
            Assert.AreEqual((uint)1, projetos.First().IdPropriedade);
        }
    }
}