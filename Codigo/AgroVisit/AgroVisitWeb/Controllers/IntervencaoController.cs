using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
using Core.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using System.ComponentModel;

namespace AgroVisitWeb.Controllers
{

    public class IntervencaoController : Controller
    {
        private readonly IIntervencaoService _intervencaoService;
        private readonly IMapper _mapper;
 
        public IntervencaoController(IIntervencaoService intervencaoService, IMapper mapper)
        {
            _intervencaoService = intervencaoService;
            _mapper = mapper;
        }

        //GET: IntervencaoController

        public ActionResult Index()
        {
            var listaIntervencoes = _intervencaoService.GetAll();
            var listaIntervencoesModel = _mapper.Map<List<IntervencaoViewModel>>(listaIntervencoes);
            return View(listaIntervencoesModel);
        }
        // GET: IntevencaoController/Details/5

        public ActionResult Details(int id)
        {
            Intervencao intervencao = _intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // GET: IntervencaoController/Create


        public ActionResult Create()
        {
            return View();
        }

        // POST: IntervencaoController/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IntervencaoViewModel intervencaoModel)
        {
            if (ModelState.IsValid)
            {
                var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
                _intervencaoService.Create(intervencao);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: IntervencaoController/Edit/5

        public ActionResult Edit(int id)
        {
            Intervencao intervencao = (Intervencao)_intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IntervencaoViewModel intervencaoModel)
        {
            if (ModelState.IsValid)
            {
                var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
                _intervencaoService.Edit(intervencao);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: IntervencaoController/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO mappers implementation
            Intervencao intervencao = _intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IntervencaoViewModel intervencaoModel)
        {
            _intervencaoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
