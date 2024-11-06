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
    public class IntervencaoController : Controller
    {
        private readonly IIntervencaoService _intervencaoService;
        private readonly IMapper _mapper;
        private readonly IProjetoService _projetoService;

        public IntervencaoController(IIntervencaoService intervencaoService, IProjetoService projetoService, IMapper mapper)
        {
            _intervencaoService = intervencaoService;
            _projetoService = projetoService;
            _mapper = mapper;

        }

        //GET: IntervencaoController


        // GET: IntevencaoController/Details/5
        public ActionResult Details(uint id)
        {
            Intervencao intervencao = _intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // GET: IntervencaoController/Create
        public ActionResult Create()
        {
            IntervencaoViewModel intervencaoModel = new IntervencaoViewModel();

            IEnumerable<Projeto> projetos = _projetoService.GetAll();
            intervencaoModel.Projetos = new SelectList(projetos, "Id", "Nome", null);

            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IntervencaoViewModel intervencaoModel)
        {
            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);

            _intervencaoService.Create(intervencao);

            return RedirectToAction("Details", "Projeto", new { id = intervencao.IdProjeto });
        }

        // GET: IntervencaoController/Edit/5

        public ActionResult Edit(uint id)
        {
            Intervencao intervencao = (Intervencao)_intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, IntervencaoViewModel intervencaoModel)
        {
            //if (ModelState.IsValid)
            //{
            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
            _intervencaoService.Edit(intervencao);

            return RedirectToAction("Details", "Projeto", new { id = intervencao.IdProjeto });
            //}
            //return View(intervencaoModel);
        }

        // GET: IntervencaoController/Delete/5
        public ActionResult Delete(uint id)
        {
            // TODO mappers implementation
            Intervencao intervencao = _intervencaoService.Get(id);
            IntervencaoViewModel intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);

            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(uint id, IntervencaoViewModel intervencaoModel)
        {
            var intervencao = _intervencaoService.Get(id);
            var projeto = _projetoService.Get(intervencao.IdProjeto);
            _intervencaoService.Delete(id);

            return RedirectToAction("Details", "Projeto", new { id = projeto.Id });
        }
    }
}
