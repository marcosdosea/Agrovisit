using AgroVisitWeb.Models;
using AutoMapper;
using Core;
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

        // GET: VisitaController/Details/5
        public ActionResult Details(uint id)
        {
            Visita visita = _visitaService.Get(id);            
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            var idCliente = _propriedadeService.Get(visitaModel.IdPropriedade).IdCliente;
            visitaModel.NomePropriedade = _propriedadeService.Get(visitaModel.IdPropriedade).Nome;
            visitaModel.NomeCliente = _clienteService.Get(idCliente).Nome;
            return View(visitaModel);
        }

        // GET: VisitaController/Create
        public ActionResult Create()
        {
            VisitaViewModel visitaModel = new VisitaViewModel();
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();

            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome", null);
            return View(visitaModel);
        }

        // POST: VisitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitaViewModel visitaModel)
        {
            if (ModelState.IsValid)
            {
                var visita = _mapper.Map<Visita>(visitaModel);
                _visitaService.Create(visita);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: VisitaController/Edit/5
        public ActionResult Edit(uint id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();
            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome",
                listaPropriedades.FirstOrDefault(e => e.Id.Equals(visita.IdPropriedade)));
            return View(visitaModel);
        }

        // POST: VisitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, VisitaViewModel visitaModel)
        {
            if (ModelState.IsValid)
            {
                var visita = _mapper.Map<Visita>(visitaModel);
                _visitaService.Edit(visita);
            }
            return RedirectToAction(nameof(Index));        
        }

        // GET: VisitaController/Delete/5
        public ActionResult Delete(uint id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            return View(visitaModel);
        }

        // POST: VisitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(uint id, VisitaViewModel visitaModel)
        {
            _visitaService.Delete(id);
            return RedirectToAction(nameof(Index));           
        }
    }
}
