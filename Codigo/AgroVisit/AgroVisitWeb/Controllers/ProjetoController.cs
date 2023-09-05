using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.DTO;
using Core.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace AgroVisitWeb.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoService _projetoService;
        private readonly IIntervencaoService _intervencaoService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IIntervencaoService intervencaoService, IMapper mapper) 
        {
            _projetoService = projetoService;
            _intervencaoService = intervencaoService;
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
        public ActionResult Details(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            return View(projetoModel);
        }

        // GET: ProjetoController/Create
        public ActionResult Create()
        {
            ProjetoViewModel projetoModel = new ProjetoViewModel();
            IEnumerable<Intervencao> listaIntervencoes = _intervencaoService.GetAll();
            
            projetoModel.ListaIntervencoes = new SelectList(listaIntervencoes, "Id", "Nome", null);
            
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
        public ActionResult Edit(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);

            IEnumerable<Intervencao> listaIntervencoes = _intervencaoService.GetAll();

            projetoModel.ListaIntervencoes = new SelectList(listaIntervencoes, "Id", "Nome", 
                listaIntervencoes.FirstOrDefault(e => e.Id.Equals(projeto.Id)));

            return View(projetoModel);
        }

        // POST: ProjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjetoViewModel projetoModel)
        {
            if(ModelState.IsValid)
            {
                var projeto = _mapper.Map<Projeto>(projetoModel);
                _projetoService.Edit(projeto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjetoController/Delete/5
        public ActionResult Delete(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoModel = _mapper.Map<ProjetoViewModel>(projeto);
            return View(projetoModel);
        }

        // POST: ProjetoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProjetoViewModel projetoModel)
        {
            _projetoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
