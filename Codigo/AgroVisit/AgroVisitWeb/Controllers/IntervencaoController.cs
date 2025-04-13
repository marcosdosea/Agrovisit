using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Details(uint id)
        {
            var intervencao = await _intervencaoService.Get(id);
            var intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return View(intervencaoModel);
        }

        // GET: IntervencaoController/Create
        public IActionResult Create(int idProjeto)
        {
            var model = new IntervencaoViewModel
            {
                IdProjeto = idProjeto
            };

            return PartialView("Create", model);
        }


        // POST: IntervencaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IntervencaoViewModel intervencaoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Create", intervencaoModel);
            }

            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
            await _intervencaoService.Create(intervencao);

            return Json(new { success = true, projetoId = intervencao.IdProjeto });
        }

        // GET: IntervencaoController/Edit/5
        public async Task<IActionResult> Edit(uint id)
        {
            var intervencao = await _intervencaoService.Get(id);
            var intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);
            return PartialView("Edit", intervencaoModel);
        }

        // POST: IntervencaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IntervencaoViewModel intervencaoModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Edit", intervencaoModel);
            }

            var intervencao = _mapper.Map<Intervencao>(intervencaoModel);
            await _intervencaoService.Edit(intervencao);

            return Json(new { success = true, projetoId = intervencao.IdProjeto });
        }

        // GET: IntervencaoController/Delete/5
        public async Task<IActionResult> Delete(uint id)
        {
            var intervencao = await _intervencaoService.Get(id);
            var intervencaoModel = _mapper.Map<IntervencaoViewModel>(intervencao);

            return View(intervencaoModel);
        }

        // POST: IntervencaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id, IntervencaoViewModel intervencaoModel)
        {
            var intervencao = await _intervencaoService.Get(id);
            var projeto = await _projetoService.Get(intervencao.IdProjeto);
            await _intervencaoService.Delete(id);

            return RedirectToAction("Details", "Projeto", new { id = projeto.Id });
        }
    }
}
