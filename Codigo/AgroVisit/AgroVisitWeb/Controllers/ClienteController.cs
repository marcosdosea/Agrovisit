using AgroVisitWeb.Models;
using Core;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        // criar mappers implementation 

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            // TODO mappers implementation
            var listaCliente = _clienteService.GetAll();
            return View(listaCliente);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            // TODO mappers implementation
            Cliente cliente = _clienteService.Get(id);
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
            // TODO mappers implementation
            if (ModelState.IsValid)
            {
                var cliente = new Cliente();
                _clienteService.Create(cliente);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            // TODO mappers implementation
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteViewModel = new ClienteViewModel();
            return View(clienteViewModel);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            // TODO mappers implementation
            if (ModelState.IsValid)
            {
                var cliente = new Cliente();
                _clienteService.Edit(cliente);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            // TODO mappers implementation
            Cliente cliente = _clienteService.Get(id);
            ClienteViewModel clienteViewModel = new ClienteViewModel();
            return View(clienteViewModel);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _clienteService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}