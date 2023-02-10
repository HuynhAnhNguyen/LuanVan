using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
