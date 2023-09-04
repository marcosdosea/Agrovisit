using AgroVisitWeb.Models;
using AutoMapper;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            var listaCliente = _clienteService.GetAll();
            var listaClienteModel = _mapper.Map<List<ClienteViewModel>>(listaCliente);
            return View(listaClienteModel);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            return View(cliente);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            return View(clienteModel);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClienteViewModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteModel);
                _clienteService.Edit(cliente);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO mappers implementation
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteModel = _mapper.Map<ClienteViewModel>(cliente);
            return View(clienteModel);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ClienteViewModel clienteViewModel)
        {
            _clienteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}