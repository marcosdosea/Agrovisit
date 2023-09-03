using AgroVisitWeb.Models;
using Core;
using Core.DTO;
using Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoService _projetoService;

        public ProjetoController (IProjetoService projetoService) 
        {
            _projetoService = projetoService;
        }
        // GET: ProjetoController
        public ActionResult Index()
        {
            var listaProjetos = _projetoService.GetAll();
            return View(listaProjetos);
        }

        // GET: ProjetoController/Details/5
        public ActionResult Details(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            return View(projeto);
        }

        // GET: ProjetoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjetoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjetoViewModel projetoModel)
        {
            if (ModelState.IsValid) 
            {
                var projeto = new Projeto();
                _projetoService.Create(projeto);
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: ProjetoController/Edit/5
        public ActionResult Edit(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoViewModel = new ProjetoViewModel();
            return View(projetoViewModel);
        }

        // POST: ProjetoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjetoViewModel projetoModel)
        {
            if(ModelState.IsValid)
            {
                var projeto = new Projeto();
                _projetoService.Edit(projeto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProjetoController/Delete/5
        public ActionResult Delete(int id)
        {
            Projeto projeto = _projetoService.Get(id);
            ProjetoViewModel projetoViewModel = new ProjetoViewModel();
            return View(projetoViewModel);
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
