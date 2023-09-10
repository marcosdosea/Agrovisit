using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol;
using Service;

namespace AgroVisitWeb.Controllers
{
    public class PropriedadeController : Controller
    {
        private readonly IPropriedadeService _propriedadeService;
        private readonly IClienteService _clienteService;
        private readonly IProjetoService _projetoService;
        private readonly IVisitaService _visitaService;
        private readonly IMapper _mapper;
        public PropriedadeController(IPropriedadeService propriedadeService, IClienteService clienteService, IProjetoService projetoService, IVisitaService visitaService, IMapper mapper)
        {
            _propriedadeService = propriedadeService;
            _clienteService = clienteService;
            _projetoService = projetoService;
            _visitaService = visitaService;
            _mapper = mapper;
        }

        // GET: PropriedadeController
        public ActionResult Index()
        {
            var listaPropriedades = _propriedadeService.GetAll();
            var listaPropriedadesModel = _mapper.Map<List<PropriedadeViewModel>>(listaPropriedades);
            return View(listaPropriedadesModel);
        }

        // GET: PropriedadeController/Details/5
        public ActionResult Details(uint id)
        {
            Propriedade propriedade = _propriedadeService.Get(id);
            PropriedadeViewModel propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);
            propriedadeModel.ListaProjetos = (List<Projeto>?)_projetoService.GetByPropriedade(id);
            propriedadeModel.ListaVisitas = (List<Visita>?)_visitaService.GetByPropriedade(id);

            return View(propriedadeModel);
        }

        // GET: PropriedadeController/Create
        public ActionResult Create()
        {
            PropriedadeViewModel propriedadeModel = new PropriedadeViewModel();
            IEnumerable<Cliente> listaClientes = _clienteService.GetAll();
            propriedadeModel.ListaClientes = new SelectList(listaClientes, "IdCliente", "Nome", null);
            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropriedadeViewModel propriedadeModel)
        {
            if (ModelState.IsValid)
            {
                var propriedade = _mapper.Map<Propriedade>(propriedadeModel);
                _propriedadeService.Create(propriedade);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PropriedadeController/Edit/5
        public ActionResult Edit(uint id)
        {
            Propriedade propriedade = _propriedadeService.Get(id);
            PropriedadeViewModel propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);

            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, PropriedadeViewModel propriedadeModel)
        {
            if (ModelState.IsValid)
            {
                var propriedade = _mapper.Map<Propriedade>(propriedadeModel);
                _propriedadeService.Edit(propriedade);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PropriedadeController/Delete/5
        public ActionResult Delete(uint id)
        {
            Propriedade propriedade = _propriedadeService.Get(id);
            PropriedadeViewModel propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);
            
            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(uint id, PropriedadeViewModel propriedadeModel)
        {
            _propriedadeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
