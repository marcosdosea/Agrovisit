using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgroVisitWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
