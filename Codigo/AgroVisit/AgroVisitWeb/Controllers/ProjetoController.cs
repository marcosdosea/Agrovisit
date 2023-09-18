using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgroVisitWeb.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoService _projetoService;
        /*private readonly IContaService _contaService;*/
        private readonly IIntervencaoService _intervencaoService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IIntervencaoService intervencaoService, /*IContaService contaService,*/ IPropriedadeService propriedadeService, IMapper mapper)
        {
            _projetoService = projetoService;
            _intervencaoService = intervencaoService;
            //_contaService = contaService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }
        // GET: ProjetoController
        public ActionResult Index()
        {
            var listaProjetos = _projetoService.GetAll();
            var listaProjetosModel = _mapper.Map<List<ProjetoViewModel>>(listaProjetos);
            return View(listaProjetosModel);
        }

        // GET: ProjetoController/Details/5
        public ActionResult Details(uint id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);

            IEnumerable<Intervencao> listaIntervencoes = _intervencaoService.GetAll();
            projetoModel.ListaIntervencoes = _intervencaoService.GetByProjeto(id);

            /*IEnumerable<Conta> listaContas = _contaService.GetAll();
            projetoModel.ListaContas = _contaService.GetByProjeto(id);*/

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
            Projeto projeto = _projetoService.Get(id);
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
            Projeto projeto = _projetoService.Get(id);
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
