using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Datatables;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

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
        public ActionResult Index()
        {
            var listaCliente = _clienteService.GetAll();
            var listaClienteModel = _mapper.Map<List<ClienteViewModel>>(listaCliente);
            return View(listaClienteModel);
        }

        [HttpPost]
        public IActionResult GetDataPage(DatatableRequest request)
        {
            var response = _clienteService.GetDataPage(request);
            return Json(response);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(uint id)
        {
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            clienteModel.ListaPropriedade = _propriedadeService.GetByCliente(cliente.Id);
            return View(clienteModel);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            var clienteModel = new ClienteViewModel
            {
                IdEngenheiroAgronomo = 1
            };
            return View(clienteModel);
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteModel);
                _clienteService.Create(cliente);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(uint id)
        {
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);

            return View(clienteModel);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(uint id, ClienteViewModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteModel);
                _clienteService.Edit(cliente);

                return RedirectToAction("Details", "Cliente", new { id = cliente.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(uint id)
        {
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            return View(clienteModel);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(uint id, ClienteViewModel clienteViewModel)
        {
            _clienteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}