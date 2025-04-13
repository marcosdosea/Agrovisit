using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace ServiceTests
{
    [TestClass()]
    public class VisitaServiceTests
    {
        private AgroVisitContext _context;
        private IVisitaService _visitaService;

        [TestInitialize()]
        public async Task Initialize()
        {
            var builder = new DbContextOptionsBuilder<AgroVisitContext>();
            builder.UseInMemoryDatabase("AgroVisit");
            var options = builder.Options;

            _context = new AgroVisitContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var visitas = new List<Visita>
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
                    IdPropriedade = 2
                }
            };

            await _context.AddRangeAsync(visitas);
            await _context.SaveChangesAsync();

            _visitaService = new VisitaService(_context);
        }

        [TestMethod()]
        public async Task CreateTest()
        {
            await _visitaService.Create(new Visita
            {
                Id = 4,
                Observacoes = "Coletar amostra de solo para análise",
                DataHora = DateTime.Parse("2023-10-05"),
                Status = "A"
            });

            var all = await _visitaService.GetAll();
            Assert.AreEqual(4, all.Count());

            var visita = await _visitaService.Get(4);
            Assert.AreEqual("Coletar amostra de solo para análise", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-10-05"), visita.DataHora);
        }

        [TestMethod()]
        public async Task DeleteTest()
        {
            await _visitaService.Delete(2);
            var all = await _visitaService.GetAll();
            Assert.AreEqual(2, all.Count());

            var visita = await _visitaService.Get(2);
            Assert.IsNull(visita);
        }

        [TestMethod()]
        public async Task EditTest()
        {
            var visita = await _visitaService.Get(3);
            visita.Observacoes = "Coletar amostra de solo para análise";
            visita.DataHora = DateTime.Parse("2023-10-11");

            await _visitaService.Edit(visita);

            visita = await _visitaService.Get(3);
            Assert.IsNotNull(visita);
            Assert.AreEqual("Coletar amostra de solo para análise", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-10-11"), visita.DataHora);
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var visita = await _visitaService.Get(1);
            Assert.IsNotNull(visita);
            Assert.AreEqual("Entregar sementes tratadas", visita.Observacoes);
            Assert.AreEqual(DateTime.Parse("2023-09-30"), visita.DataHora);
        }

        [TestMethod()]
        public async Task GetAllTest()
        {
            var listaVisita = await _visitaService.GetAll();

            Assert.IsInstanceOfType(listaVisita, typeof(IEnumerable<Visita>));
            Assert.IsNotNull(listaVisita);
            Assert.AreEqual(3, listaVisita.Count());
            Assert.AreEqual((uint)1, listaVisita.First().Id);
            Assert.AreEqual("Entregar sementes tratadas", listaVisita.First().Observacoes);
        }

        [TestMethod()]
        public async Task GetAllByDateTest()
        {
            var visitas = await _visitaService.GetAllByDate(DateTime.Parse("2023-09-30"));

            Assert.IsNotNull(visitas);
            Assert.AreEqual(1, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }

        [TestMethod()]
        public async Task GetAllByStatusTest()
        {
            var visitas = await _visitaService.GetAllByStatus("A");
            Assert.IsNotNull(visitas);
            Assert.AreEqual(2, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }

        [TestMethod()]
        public async Task GetByPropriedadeTest()
        {
            var visitas = await _visitaService.GetByPropriedade(1);

            Assert.IsNotNull(visitas);
            Assert.AreEqual(2, visitas.Count());
            Assert.AreEqual("Entregar sementes tratadas", visitas.First().Observacoes);
        }
    }
}
