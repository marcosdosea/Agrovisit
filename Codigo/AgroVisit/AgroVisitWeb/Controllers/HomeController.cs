using AgroVisitWeb.Areas.Identity.Data;
using AgroVisitWeb.Models;
using AutoMapper;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgroVisitWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<UsuarioIdentity> _userManager;

        public HomeController(UserManager<UsuarioIdentity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Administrador"))
            {
                return RedirectToAction("Index", "Administrador");
            }
            else if (await _userManager.IsInRoleAsync(user, "Agronomo"))
            {
                return RedirectToAction("Index", "Agronomo");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}