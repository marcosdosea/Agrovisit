using AgroVisitWeb.Models;
using AutoMapper;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Agronomo")]
    public class AgronomoController : Controller
    {
        private readonly IVisitaService _visitaService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IMapper _mapper;

        public AgronomoController(IVisitaService visitaService, IMapper mapper, IPropriedadeService propriedadeService)
        {
            _visitaService = visitaService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var listaVisitas = await _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaViewModel>>(listaVisitas);

            if (!ModelState.IsValid)
                return NotFound();

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
    }
}
