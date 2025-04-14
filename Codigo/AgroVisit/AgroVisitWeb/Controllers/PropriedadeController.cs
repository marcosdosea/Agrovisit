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
        public async Task<IActionResult> Index()
        {
            var listaPropriedades = await _propriedadeService.GetAllDto();

            if (!ModelState.IsValid)
                return NotFound();

            return View(listaPropriedades);
        }

        // GET: PropriedadeController/Details/5
        public async Task<IActionResult> Details(uint id)
        {
            var propriedade = await _propriedadeService.Get(id);
            var cliente = await _clienteService.Get(propriedade.IdCliente);

            var propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);

            if (!ModelState.IsValid)
                return NotFound();

            propriedadeModel.ListaProjetos = await _projetoService.GetByPropriedade(id);
            propriedadeModel.ListaVisitas = await _visitaService.GetByPropriedade(id);
            propriedadeModel.NomeCultura = (await Task.Run(() => _culturaService.Get(propriedadeModel.IdCultura))).Nome;
            propriedadeModel.NomeSolo = (await Task.Run(() => _soloService.Get(propriedadeModel.IdSolo))).Nome;
            propriedadeModel.NomeCliente = cliente.Nome;
            return View(propriedadeModel);
        }

        // GET: PropriedadeController/Create
        public async Task<IActionResult> Create()
        {
            var listaClientes = await _clienteService.GetAll();

            var propriedadeModel = new PropriedadeViewModel
            {
                ListaClientes = new SelectList(listaClientes, "Id", "Nome"),
                ListaCulturas = new SelectList(await Task.Run(() => _culturaService.GetAll()), "Id", "Nome"),
                ListaSolos = new SelectList(await Task.Run(() => _soloService.GetAll()), "Id", "Nome")
            };

            if (!ModelState.IsValid)
                return NotFound();

            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropriedadeViewModel propriedadeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(propriedadeModel);
            }

            var propriedade = _mapper.Map<Propriedade>(propriedadeModel);
            propriedade.IdEngenheiroAgronomo = 1;
            await _propriedadeService.Create(propriedade);

            return RedirectToAction(nameof(Index));
        }

        // GET: PropriedadeController/Edit/5
        public async Task<IActionResult> Edit(uint id)
        {
            var propriedade = await _propriedadeService.Get(id);
            var propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);

            if (!ModelState.IsValid)
                return NotFound();

            propriedadeModel.ListaCulturas = new SelectList(await Task.Run(() => _culturaService.GetAll()), "Id", "Nome");
            propriedadeModel.ListaSolos = new SelectList(await Task.Run(() => _soloService.GetAll()), "Id", "Nome");
            propriedadeModel.ListaClientes = new SelectList(await Task.Run(() => _clienteService.GetAll()), "Id", "Nome");
            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PropriedadeViewModel propriedadeModel)
        {
            if (!ModelState.IsValid)
                return View(propriedadeModel);

            var propriedade = _mapper.Map<Propriedade>(propriedadeModel);
            await _propriedadeService.Edit(propriedade);
            return RedirectToAction(nameof(Index));
        }

        // GET: PropriedadeController/Delete/5
        public async Task<IActionResult> Delete(uint id)
        {
            var propriedade = await _propriedadeService.Get(id);
            var cliente = await _clienteService.Get(propriedade.IdCliente);

            var propriedadeModel = _mapper.Map<PropriedadeViewModel>(propriedade);

            if (!ModelState.IsValid)
                return NotFound();

            propriedadeModel.NomeCultura = (await Task.Run(() => _culturaService.Get(propriedadeModel.IdCultura))).Nome;
            propriedadeModel.NomeSolo = (await Task.Run(() => _soloService.Get(propriedadeModel.IdSolo))).Nome;
            propriedadeModel.NomeCliente = cliente.Nome;
            return View(propriedadeModel);
        }

        // POST: PropriedadeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(uint id, PropriedadeViewModel propriedadeModel)
        {
            await _propriedadeService.Delete(id);
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
