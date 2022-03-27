using Microsoft.AspNetCore.Mvc;

namespace Web.Anuncio.Controllers
{
    public class AnuncioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
