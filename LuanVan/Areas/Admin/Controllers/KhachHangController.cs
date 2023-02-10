using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Services _service;

        public KhachHangController(Services service)
        {
            _service = service;
        }
        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                return View(_service.danhSachKH());
            }
            return RedirectToAction("Index", "Login"); ;
        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsKhachHang(string id)
        {
            return View(_service.getKH(id));
        }
    }
}
