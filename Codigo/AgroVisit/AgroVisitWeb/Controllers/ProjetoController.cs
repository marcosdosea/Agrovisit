using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Datatables;
using Core.DTO;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles ="Agronomo")]
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
        public ActionResult Index()
        {
            var listaProjetos = _projetoService.GetAllDto();

            return View(listaProjetos);
        }

        [HttpPost]
        public IActionResult GetDataPage(DatatableRequest request)
        {
            var response = _projetoService.GetDataPage(request);
            return Json(response);
        }

        // GET: ProjetoController/Details/5
        public ActionResult Details(uint id)
        {
            Projeto? projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);

            IEnumerable<Intervencao> listaIntervencoes = _intervencaoService.GetAll();
            var propriedade = _propriedadeService.Get(projeto.IdPropriedade);
            projetoModel.ListaIntervencoes = _intervencaoService.GetByProjeto(id);
            projetoModel.NomeCliente = _clienteService.Get(propriedade.IdCliente).Nome;
            projetoModel.NomePropriedade = _propriedadeService.Get(projeto.IdPropriedade).Nome;

            return View(projetoModel);
        }

        // GET: ProjetoController/Create
        public ActionResult Create()
        {
            ProjetoViewModel projetoModel = new ProjetoViewModel();
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();
            projetoModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome", null);
            return View(projetoModel);
        }

        // POST: ProjetoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjetoViewModel projetoModel)
        {
            if (ModelState.IsValid)
            {
                var projeto = _mapper.Map<Projeto>(projetoModel);
                _projetoService.Create(projeto);
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: ProjetoController/Edit/5
        public ActionResult Edit(uint id)
        {
            Projeto? projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            IEnumerable<Propriedade> listaPropriedades = _propriedadeService.GetAll();
            projetoModel.ListaPropriedades = new SelectList(listaPropriedades, "Id", "Nome", null);

            return View(projetoModel);
        }

        // POST: ProjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, ProjetoViewModel projetoModel)
        {
            if (ModelState.IsValid)
            {
                var projeto = _mapper.Map<Projeto>(projetoModel);
                _projetoService.Edit(projeto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjetoController/Delete/5
        public ActionResult Delete(uint id)
        {
            var projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            return View(projetoModel);
        }

        // POST: ProjetoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(uint id, ProjetoViewModel projetoModel)
        {
            _projetoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
