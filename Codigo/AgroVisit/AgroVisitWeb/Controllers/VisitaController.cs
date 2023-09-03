using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    public class VisitaController : Controller
    {
        private readonly IVisitaService _visitaService;
        private readonly IMapper _mapper;

        public VisitaController(IVisitaService visitaService, IMapper mapper)
        {
            _visitaService = visitaService;
            _mapper = mapper;
        }

        // GET: VisitaController
        public ActionResult Index()
        {
            var listaVisitas = _visitaService.GetAll();
            var listaVisitasModel = _mapper.Map<List<VisitaModel>>(listaVisitas);
            return View(listaVisitasModel);
        }

        // GET: VisitaController/Details/5
        public ActionResult Details(int id)
        {
            Visita visita = _visitaService.Get(id);
            VisitaModel visitaModel = _mapper.Map<VisitaModel>(visita);
            return View(visitaModel);
        }

        // GET: VisitaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VisitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitaModel visitaModel)
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
            VisitaModel visitaModel = _mapper.Map<VisitaModel>(visita);
            return View(visitaModel);
        }

        // POST: VisitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VisitaModel visitaModel)
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
            VisitaModel visitaModel = _mapper.Map<VisitaModel>(visita);
            return View(visitaModel);
        }

        // POST: VisitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VisitaModel visitaModel)
        {
            _visitaService.Delete(id);
            return RedirectToAction(nameof(Index));           
        }
    }
}
