using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MySqlX.XDevAPI;
using Service;
using System.Globalization;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PlanoController : Controller
    {
        private readonly IPlanoService _planoService;
        private readonly IMapper _mapper;

        public PlanoController(IPlanoService planoService, IMapper mapper)
        {
            _planoService = planoService;
            _mapper = mapper;
        }

        // GET: PlanoController
        public async Task<ActionResult> Index()
        {
            var listaPlanos = await _planoService.GetAll();
            var listaPlanosModel = _mapper.Map<List<PlanoViewModel>>(listaPlanos);

            return View(listaPlanosModel);
        }

        // GET: PlanoController/Details/5
        public async Task<ActionResult> Details(uint id)
        {
            var plano = await _planoService.Get(id);

            var planoModel = _mapper.Map<PlanoViewModel>(plano);

            if (!ModelState.IsValid)
                return NotFound();

            return View(planoModel);
        }

        // GET: PlanoController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: PlanoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlanoViewModel planoModel)
        {
            if (!ModelState.IsValid)
                return View(planoModel);

            if (float.TryParse(planoModel.ValorPlaceHolder.Replace("R$", "").Trim(), NumberStyles.Currency, new CultureInfo("pt-BR"), out float valorDecimal))
            {
                planoModel.Valor = valorDecimal;
            }

            var plano = _mapper.Map<Plano>(planoModel);
            await _planoService.Create(plano);

            return RedirectToAction(nameof(Index));
        }

        // GET: PlanoController/Edit/5
        public async Task<ActionResult> Edit(uint id)
        {
            var plano = await _planoService.Get(id);
            var planoModel = _mapper.Map<PlanoViewModel>(plano);

            if (!ModelState.IsValid)
                return NotFound();

            planoModel.ValorPlaceHolder = plano.Valor.ToString("C", new CultureInfo("pt-BR"));

            return View(planoModel);
        }

        // POST: PlanoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PlanoViewModel planoModel)
        {
            if (!ModelState.IsValid)
                return View(planoModel);

            var plano = _mapper.Map<Plano>(planoModel);
            await _planoService.Edit(plano);

            return RedirectToAction("Details", new { id = plano.Id });
        }

        // GET: PlanoController/Delete/5
        public async Task<ActionResult> Delete(uint id)
        {
            var plano = await _planoService.Get(id);
            var planoModel = _mapper.Map<PlanoViewModel>(plano);

            if (!ModelState.IsValid)
                return NotFound();

            return View(planoModel);
        }

        // POST: PlanoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(uint id, PlanoViewModel planoModel)
        {
            await _planoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
