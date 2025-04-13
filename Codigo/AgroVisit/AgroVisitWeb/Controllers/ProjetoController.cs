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
    public class ProjetoController : Controller
    {
        private readonly IProjetoService _projetoService;
        private readonly IIntervencaoService _intervencaoService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IIntervencaoService intervencaoService, IPropriedadeService propriedadeService, IClienteService clienteService, IMapper mapper)
        {
            _projetoService = projetoService;
            _intervencaoService = intervencaoService;
            _propriedadeService = propriedadeService;
            _clienteService = clienteService;
            _mapper = mapper;
        }
        // GET: ProjetoController
        public async Task<ActionResult> Index()
        {
            var listaProjetos = await _projetoService.GetAllDto();
            return View(listaProjetos);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataPage(DatatableRequest request)
        {
            var response = await _projetoService.GetDataPage(request);
            return Json(response);
        }

        // GET: ProjetoController/Details/5
        public async Task<ActionResult> Details(uint id)
        {
            var projeto = await _projetoService.Get(id);
            if (projeto == null)
                return NotFound();

            var projetoModel = _mapper.Map<ProjetoViewModel>(projeto);

            var listaIntervencoes = await _intervencaoService.GetByProjeto(id);
            var propriedade = await _propriedadeService.Get(projeto.IdPropriedade);
            var cliente = await _clienteService.Get(propriedade.IdCliente);

            projetoModel.ListaIntervencoes = listaIntervencoes;
            projetoModel.NomeCliente = cliente.Nome;
            projetoModel.NomePropriedade = propriedade.Nome;

            return View(projetoModel);
        }

        // GET: ProjetoController/Create
        public async Task<ActionResult> Create()
        {
            var projetoModel = new ProjetoViewModel();
            var listaPropriedades = await _propriedadeService.GetAll();

            projetoModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome");

            return View(projetoModel);
        }

        // POST: ProjetoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjetoViewModel projetoModel)
        {
            if (!ModelState.IsValid)
            {

                return View(projetoModel);
            }

            var projeto = _mapper.Map<Projeto>(projetoModel);
            await _projetoService.Create(projeto);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProjetoController/Edit/5
        public async Task<ActionResult> Edit(uint id)
        {
            var projeto = await _projetoService.Get(id);
            if (projeto == null)
                return NotFound();

            var projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            var listaPropriedades = await _propriedadeService.GetAll();

            projetoModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome");

            return View(projetoModel);
        }

        // POST: ProjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjetoViewModel projetoModel)
        {
            if (!ModelState.IsValid)
            {
                return View(projetoModel);
            }

            var projeto = _mapper.Map<Projeto>(projetoModel);
            await _projetoService.Edit(projeto);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProjetoController/Delete/5
        public async Task<ActionResult> Delete(uint id)
        {
            var projeto = await _projetoService.Get(id);
            if (projeto == null)
                return NotFound();

            var projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            return View(projetoModel);
        }

        // POST: ProjetoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(uint id, ProjetoViewModel projetoModel)
        {
            await _projetoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
