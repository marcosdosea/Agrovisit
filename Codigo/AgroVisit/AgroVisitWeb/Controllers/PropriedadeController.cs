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
    public class PropriedadeController : Controller
    {
        private readonly IPropriedadeService _propriedadeService;
        private readonly IClienteService _clienteService;
        private readonly IProjetoService _projetoService;
        private readonly IVisitaService _visitaService;
        private readonly ISoloService _soloService;
        private readonly ICulturaService _culturaService;
        private readonly IMapper _mapper;

        public PropriedadeController(IPropriedadeService propriedadeService, ISoloService soloService, ICulturaService culturaService, IClienteService clienteService, IProjetoService projetoService, IVisitaService visitaService, IMapper mapper)
        {
            _propriedadeService = propriedadeService;
            _clienteService = clienteService;
            _projetoService = projetoService;
            _visitaService = visitaService;
            _soloService = soloService;
            _culturaService = culturaService;
            _mapper = mapper;
        }

        // GET: PropriedadeController
        public ActionResult Index()
        {
            var listaPropriedades = _propriedadeService.GetAllDto();
            
            return View(listaPropriedades);
        }

        // GET: PropriedadeController/Details/5
        public ActionResult Details(uint id)
        {
            Propriedade propriedade = _propriedadeService.Get(id);
            PropriedadeViewModel propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);
            propriedadeModel.ListaProjetos = _projetoService.GetByPropriedade(id);
            propriedadeModel.ListaVisitas = _visitaService.GetByPropriedade(id);
            propriedadeModel.NomeCultura = _culturaService.Get(propriedadeModel.IdCultura).Nome;
            propriedadeModel.NomeSolo = _soloService.Get(propriedadeModel.IdSolo).Nome;
            propriedadeModel.NomeCliente = _clienteService.Get(propriedadeModel.IdCliente).Nome;

            return View(propriedadeModel);
        }

        // GET: PropriedadeController/Create
        public ActionResult Create()
        {
            PropriedadeViewModel propriedadeModel = new PropriedadeViewModel();
            IEnumerable<Cliente> listaClientes = _clienteService.GetAll();
            IEnumerable<Cultura> listaCulturas = _culturaService.GetAll();
            IEnumerable<Solo> listaSolos = _soloService.GetAll();
            propriedadeModel.ListaClientes = new SelectList(listaClientes, "Id", "Nome", null);
            propriedadeModel.ListaCulturas = new SelectList(listaCulturas, "Id", "Nome", null);
            propriedadeModel.ListaSolos = new SelectList(listaSolos, "Id", "Nome", null);
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
                propriedade.IdEngenheiroAgronomo = 1;
                _propriedadeService.Create(propriedade);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PropriedadeController/Edit/5
        public ActionResult Edit(uint id)
        {
            Propriedade propriedade = _propriedadeService.Get(id);
            PropriedadeViewModel propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);
            IEnumerable<Cultura> listaCulturas = _culturaService.GetAll();
            IEnumerable<Solo> listaSolos = _soloService.GetAll();
            propriedadeModel.ListaCulturas = new SelectList(listaCulturas, "Id", "Nome", null);
            propriedadeModel.ListaSolos = new SelectList(listaSolos, "Id", "Nome", null);

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
            propriedadeModel.NomeCultura = _culturaService.Get(propriedadeModel.IdCultura).Nome;
            propriedadeModel.NomeSolo = _soloService.Get(propriedadeModel.IdSolo).Nome;
            propriedadeModel.NomeCliente = _clienteService.Get(propriedadeModel.IdCliente).Nome;
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

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile Georreferenciamento)
        {
            if (Georreferenciamento != null && Georreferenciamento.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", Georreferenciamento.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Georreferenciamento.CopyToAsync(stream);
                }

                ViewBag.Message = "Arquivo enviado com sucesso!";
                return View();
            }

            ViewBag.Message = "Erro ao enviar arquivo.";
            return View();
        }


    }
}
