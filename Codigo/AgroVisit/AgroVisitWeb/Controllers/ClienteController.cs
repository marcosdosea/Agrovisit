using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Datatables;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Agronomo")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IPropriedadeService _propriedadeService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IPropriedadeService propriedadeService, IMapper mapper)
        {
            _clienteService = clienteService;
            _propriedadeService = propriedadeService;
            _mapper = mapper;
        }

        // GET: ClienteController
        public async Task<ActionResult> Index()
        {
            var listaCliente = await _clienteService.GetAll();
            var listaClienteModel = _mapper.Map<List<ClienteViewModel>>(listaCliente);
            return View(listaClienteModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataPage(DatatableRequest request)
        {
            var response = await _clienteService.GetDataPage(request);
            return Json(response);
        }

        // GET: ClienteController/Details/5
        public async Task<ActionResult> Details(uint id)
        {
            var cliente = await _clienteService.Get(id);

            var clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            clienteModel.ListaPropriedade = await _propriedadeService.GetByCliente(cliente.Id);

            if (!ModelState.IsValid)
                return NotFound();

            return View(clienteModel);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            var clienteModel = new ClienteViewModel
            {
                IdEngenheiroAgronomo = 1,
            };

            return View(clienteModel);
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClienteViewModel clienteModel)
        {
            if (!ModelState.IsValid)
                return View(clienteModel);

            var cliente = _mapper.Map<Cliente>(clienteModel);
            await _clienteService.Create(cliente);

            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Edit/5
        public async Task<ActionResult> Edit(uint id)
        {
            var cliente = await _clienteService.Get(id);
            var clienteModel = _mapper.Map<ClienteViewModel>(cliente);

            if (!ModelState.IsValid)
                return NotFound();

            return View(clienteModel);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClienteViewModel clienteModel)
        {
            if (!ModelState.IsValid)
                return View(clienteModel);

            var cliente = _mapper.Map<Cliente>(clienteModel);
            await _clienteService.Edit(cliente);

            return RedirectToAction("Details", new { id = cliente.Id });
        }

        // GET: ClienteController/Delete/5
        public async Task<ActionResult> Delete(uint id)
        {
            var cliente = await _clienteService.Get(id);
            var clienteModel = _mapper.Map<ClienteViewModel>(cliente);

            if (!ModelState.IsValid)
                return NotFound();

            return View(clienteModel);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(uint id, ClienteViewModel clienteViewModel)
        {
            await _clienteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}