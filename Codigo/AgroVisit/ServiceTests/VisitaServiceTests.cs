using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Service.Tests
{
    [TestClass()]
    public class VisitaServiceTests
    {
        private AgroVisitContext _context;
        private IVisitaService _visitaService;

        [TestInitialize()]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisit");
            var options = builder.Options;

            _context = new AgroVisitContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var visitas = new List<Visita>
            {
                new Visita {Id = 1, Observacoes = "Entregar sementes tratadas",
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
                    IdPropriedade = 2
                }
            };
            _context.AddRange(visitas);
            _context.SaveChanges();
            _visitaService = new VisitaService(_context);
        }

        [TestMethod()]
        public void CreateTest()
        {
            _visitaService.Create(new Visita() { Id = 4, Observacoes = "Coletar amostra de solo para análise", DataHora = DateTime.Parse("2023-10-05"), Status = "A" });
            Assert.AreEqual(4, _visitaService.GetAll().Count());
            var visita = _visitaService.Get(4);
            Assert.AreEqual("Coletar amostra de solo para análise", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-10-05"), visita.DataHora);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            _visitaService.Delete(2);
            Assert.AreEqual(2, _visitaService.GetAll().Count());
            var visita = _visitaService.Get(2);
            Assert.AreEqual(null, visita);
        }

        [TestMethod()]
        public void EditTest()
        {
            var visita = _visitaService.Get(3);
            visita.Observacoes = "Coletar amostra de solo para análise";
            visita.DataHora = DateTime.Parse("2023-10-11");
            _visitaService.Edit(visita);

            visita = _visitaService.Get(3);
            Assert.IsNotNull(visita);
            Assert.AreEqual("Coletar amostra de solo para análise", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-10-11"), visita.DataHora);
        }

        [TestMethod()]
        public void GetTest()
        {
            var visita = _visitaService.Get(1);
            Assert.IsNotNull(visita);
            Assert.AreEqual("Entregar sementes tratadas", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-09-30"), visita.DataHora);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var listaVisita = _visitaService.GetAll();

            Assert.IsInstanceOfType(listaVisita, typeof(IEnumerable<Visita>));
            Assert.IsNotNull(listaVisita);
            Assert.AreEqual(3, listaVisita.Count());
            Assert.AreEqual((uint)1, listaVisita.First().Id);
            Assert.AreEqual("Entregar sementes tratadas", listaVisita.First().Observacoes);
        }

        [TestMethod()]
        public void GetAllByDateTest()
        {
            var visitas = _visitaService.GetAllByDate(DateTime.Parse("2023-09-30"));

            Assert.IsNotNull(visitas);
            Assert.AreEqual(1, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }

        [TestMethod()]
        public void GetAllByStatusTest()
        {
            var visitas = _visitaService.GetAllByStatus("A");
            Assert.IsNotNull(visitas);
            Assert.AreEqual(2, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }

        [TestMethod()]
        public void GetByPropriedadeTest()
        {
            var visitas = _visitaService.GetByPropriedade(1);

            Assert.IsNotNull(visitas);
            Assert.AreEqual(2, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }
    }
}