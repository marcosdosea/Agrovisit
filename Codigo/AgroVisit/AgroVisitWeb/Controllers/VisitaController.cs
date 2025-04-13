using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Datatables;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Agronomo")]
    public class VisitaController : Controller
    {
        private readonly IPropriedadeService _propriedadeService;
        private readonly IClienteService _clienteService;
        private readonly IVisitaService _visitaService;
        private readonly IMapper _mapper;

        public VisitaController(IVisitaService visitaService, IPropriedadeService propriedadeService, IClienteService clienteService, IMapper mapper)
        {
            _visitaService = visitaService;
            _propriedadeService = propriedadeService;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: VisitaController
        public async Task<ActionResult> Index()
        {
            var listaVisitas = await _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaViewModel>>(listaVisitas);

            foreach (var item in listaVisitasModel)
            {
                var propriedade = await _propriedadeService.Get(item.IdPropriedade);
                item.NomePropriedade = propriedade.Nome;
            }

            return View(listaVisitasModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataPage(DatatableRequest request)
        {
            var response = await _visitaService.GetDataPage(request);
            return Json(response);
        }

        // GET: VisitaController/Details/5
        public async Task<ActionResult> Details(uint id)
        {
            var visita = await _visitaService.Get(id);
            var visitaModel = _mapper.Map<VisitaViewModel>(visita);

            var propriedade = await _propriedadeService.Get(visitaModel.IdPropriedade);
            var cliente = await _clienteService.Get(propriedade.IdCliente);

            visitaModel.NomePropriedade = propriedade.Nome;
            visitaModel.NomeCliente = cliente.Nome;

            return View(visitaModel);
        }

        // GET: VisitaController/Create
        public async Task<ActionResult> Create()
        {
            VisitaViewModel visitaModel = new();
            var listaPropriedades = await _propriedadeService.GetAll();

            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome");
            visitaModel.DataHora = DateTime.Now;

            return View(visitaModel);
        }

        // POST: VisitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VisitaViewModel visitaModel)
        {
            if (visitaModel.DataHora.Year < 2000 || visitaModel.DataHora > DateTime.Now.AddYears(5))
            {
                ModelState.AddModelError("DataHora", "Data inválida");
            }

            if (!ModelState.IsValid)
            {
                return View(visitaModel);
            }

            var visita = _mapper.Map<Visita>(visitaModel);
            await _visitaService.Create(visita);

            return RedirectToAction(nameof(Index));
        }

        // GET: VisitaController/Edit/5
        public async Task<ActionResult> Edit(uint id)
        {
            var visita = await _visitaService.Get(id);
            var visitaModel = _mapper.Map<VisitaViewModel>(visita);
            var listaPropriedades = await _propriedadeService.GetAll();

            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome", visita.IdPropriedade);

            return View(visitaModel);
        }

        // POST: VisitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VisitaViewModel visitaModel)
        {
            if (visitaModel.DataHora.Year < 2000)
            {
                visitaModel.DataHora = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return View(visitaModel);
            }

            var visita = _mapper.Map<Visita>(visitaModel);
            await _visitaService.Edit(visita);

            return RedirectToAction(nameof(Index));
        }

        // GET: VisitaController/Delete/5
        public async Task<ActionResult> Delete(uint id)
        {
            var visita = await _visitaService.Get(id);
            var visitaModel = _mapper.Map<VisitaViewModel>(visita);

            if (!ModelState.IsValid)
            {
                return BadRequest("Visita não encontrada");
            }

            var propriedade = await _propriedadeService.Get(visita.IdPropriedade);

            visitaModel.NomePropriedade = propriedade.Nome;
            return View(visitaModel);
        }

        // POST: VisitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(uint id, VisitaViewModel visitaModel)
        {
            await _visitaService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
