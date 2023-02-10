using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly Services _services;
        private LuanVanContext db = new LuanVanContext();

        public SanPhamController(Services services)
        {
            _services = services;
        }

        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                ViewData["path"] = "/images/product/";
                return View(_services.danhSachSanPham().ToList());
            }
            return RedirectToAction("Index", "Login");

        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsSanPham(string id)
        {
            ViewData["path"] = "/images/product/";
            var sanpham = db.SanPhams.Where(x => x.MaLoaiSp == id);
            return View(_services.getSanPham(id));
        }

        [Area("Admin"), HttpGet]
        public IActionResult DeleteSanPham(string id)
        {
            _services.xoaSanPham(id);
            return RedirectToAction("Index", "SanPham");
        }

        [Area("Admin"), HttpGet]
        public IActionResult CreateSanPham()
        {
            ViewData["Loais"] = _services.danhSachLoaiSP().ToList();
            return View();
        }

        [Area("Admin"), HttpPost]
        public IActionResult CreateSanPham(SanPham sanPham, IFormFile file)
        {
            _services.themSanPham(sanPham, file);
            return RedirectToAction("Index", "SanPham");

        }

        [Area("Admin"), HttpGet]
        public IActionResult EditSanPham(string id)
        {
            ViewData["path"] = "/images/product/";
            var model = _services.getSanPham(id);
            ViewData["Loais"] = _services.danhSachLoaiSP().ToList();
            return View(model);
        }
        [Area("Admin"), HttpPost]
        public IActionResult Edit(SanPham sanPham, IFormFile file)
        {
            _services.suaSanPham(sanPham, file);
            return RedirectToAction("Index", "SanPham");
        }
    }
}
