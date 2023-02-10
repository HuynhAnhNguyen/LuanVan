using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        private readonly Services _service;
        public LoaiSanPhamController(Services service)
        {
            this._service = service;
        }
        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentStaff") != null)
            {
                List<LoaiSanPham> loaiSanPham = _service.danhSachLoaiSP().ToList();
                return View(loaiSanPham);
            }
            return RedirectToAction("Index", "Login");

        }

        [Area("Admin"), HttpGet]
        public IActionResult themLoaiSanPham()
        {
            return View();
        }

        [Area("Admin"), HttpPost]
        public IActionResult themLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _service.themLoaiSP(loaiSanPham);
                return RedirectToAction("Index");
            }
            return View(loaiSanPham);
        }

        [Area("Admin"), HttpGet]
        public IActionResult suaLoaiSanPham(string Id)
        {
            LoaiSanPham loaiSanPham = _service.getLoai(Id);
            return View(loaiSanPham);
        }

        [Area("Admin"), HttpPost]
        public IActionResult suaLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            _service.suaLoaiSanPham(loaiSanPham);
            return RedirectToAction("Index");
        }


        [Area("Admin"), HttpGet]
        public ActionResult xoaLoaiSanPham(string Id)
        {
            return View(_service.getLoai(Id));
        }

        [Area("Admin"), HttpPost]
        public ActionResult xoaLoaiSanPham(LoaiSanPham loaiSanPham)
        {
            if (_service.xoaLoaiSP(loaiSanPham.MaLoaiSp) == 0)
            {
                return RedirectToAction("Index", "LoaiSanPham");
            }
            return RedirectToAction("Index", "LoaiSanPham");
        }

        [Area("Admin"), HttpGet]
        public IActionResult thongTinLoaiSanPham(string Id)
        {
            LoaiSanPham loaiSP = _service.getLoai(Id);
            return View(loaiSP);
        }
    }
}
