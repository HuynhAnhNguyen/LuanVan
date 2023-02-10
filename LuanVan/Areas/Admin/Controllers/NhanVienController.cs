using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly Services _service;

        public NhanVienController(Services service)
        {
            _service = service;
        }

        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                return View(_service.danhSachNhanVien());
            }
            return RedirectToAction("Index", "Login"); ;
        }

        [Area("Admin"), HttpGet]
        public IActionResult DetailsNhanVien(string id)
        {
            return View(_service.getNhanVien(id));
        }

        [Area("Admin"), HttpGet]
        public IActionResult CreateNhanVien()
        {
            
            return View();
        }

        [Area("Admin"), HttpPost]
        public IActionResult CreateNhanVien(NhanVien nV)
        {
            if (ModelState.IsValid)
            {
                _service.themNV(nV.HoNhanVien, nV.TenNhanVien, nV.NgaySinh, nV.GioiTinh, nV.SoDienThoai, nV.DiaChi, nV.Email ,nV.MatKhau);
                return RedirectToAction("Index", "NhanVien");
            }
            return View(nV);

        }

        [Area("Admin"), HttpGet]
        public IActionResult DeleteNhanVien(string id)
        {
            _service.xoaNhanVien(id);
            return RedirectToAction("Index", "NhanVien");
        }


        [Area("Admin"), HttpGet]
        public IActionResult EditNhanVien(string id)
        {
            var model = _service.getNhanVien(id);
            return View(model);
        }

        [Area("Admin"), HttpPost]
        public IActionResult EditNhanVien(NhanVien nV)
        {
            _service.suaNhanVien(nV);
            return RedirectToAction("Index", "NhanVien");

        }
    }
}
