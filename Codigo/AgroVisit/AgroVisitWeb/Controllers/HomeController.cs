using AgroVisitWeb.Models;
using AutoMapper;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgroVisitWeb.Controllers
{
    //[Authorize(Roles = "Agronomo")]
    public class HomeController : Controller
    {
        private readonly IVisitaService _visitaService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IVisitaService visitaService, IMapper mapper, IPropriedadeService propriedadeService)
        {
            _logger = logger;
            _visitaService = visitaService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var listaVisitas = _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaViewModel>>(listaVisitas);

            foreach (var item in listaVisitasModel)
            {
                item.NomePropriedade = _propriedadeService.Get(item.IdPropriedade).Nome;
            }
            
            return View(listaVisitasModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}