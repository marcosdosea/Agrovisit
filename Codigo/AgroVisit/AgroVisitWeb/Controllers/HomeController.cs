using AgroVisitWeb.Models;
using AutoMapper;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Agronomo")]
    public class HomeController : Controller
    {
        private readonly IVisitaService _visitaService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IMapper _mapper;

        public HomeController(IVisitaService visitaService, IMapper mapper, IPropriedadeService propriedadeService)
        {
            _visitaService = visitaService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var listaVisitas = await _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaViewModel>>(listaVisitas);

            foreach (var item in listaVisitasModel)
            {
                var propriedade = await _propriedadeService.Get(item.IdPropriedade);
                if (propriedade != null)
                {
                    item.NomePropriedade = propriedade.Nome;
                }

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