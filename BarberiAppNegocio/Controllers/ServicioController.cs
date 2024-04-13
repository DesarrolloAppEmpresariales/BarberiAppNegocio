using Microsoft.AspNetCore.Mvc;

namespace BarberiAppNegocio.Controllers
{
    public class ServicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
