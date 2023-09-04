using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroVisitWeb.Controllers
{
    public class VisitaController : Controller
    {
        private readonly IPropriedadeService _propriedadeService;
        private readonly IVisitaService _visitaService;
        private readonly IMapper _mapper;

        public VisitaController(IVisitaService visitaService, IPropriedadeService propriedadeService, IMapper mapper)
        {
            _visitaService = visitaService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }

        // GET: VisitaController
        public ActionResult Index()
        {
            var listaVisitas = _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaViewModel>>(listaVisitas);
            return View(listaVisitasModel);
        }

        // GET: VisitaController/Details/5
        public ActionResult Details(int id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            return View(visitaModel);
        }

        // GET: VisitaController/Create
        public ActionResult Create()
        {
            VisitaViewModel visitaModel = new VisitaViewModel();
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();

            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "IdPropriedade", "Nome", null);
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
        public ActionResult Edit(int id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();
            visitaModel.ListaPropriedades = new SelectList(listaPropriedades, "IdPropriedade", "Nome",
                listaPropriedades.FirstOrDefault(e => e.Id.Equals(visita.IdPropriedade)));
            return View(visitaModel);
        }

        // POST: VisitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VisitaViewModel visitaModel)
        {
            if (ModelState.IsValid)
            {
                var visita = _mapper.Map<Visita>(visitaModel);
                _visitaService.Edit(visita);
            }
            return RedirectToAction(nameof(Index));        
        }

        // GET: VisitaController/Delete/5
        public ActionResult Delete(int id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaViewModel visitaModel = _mapper.Map<VisitaViewModel>(visita);
            return View(visitaModel);
        }

        // POST: VisitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VisitaViewModel visitaModel)
        {
            _visitaService.Delete(id);
            return RedirectToAction(nameof(Index));           
        }
    }
}
