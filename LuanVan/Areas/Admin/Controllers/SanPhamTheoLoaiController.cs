using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class SanPhamTheoLoaiController : Controller
    {
        private readonly Services _service;
        public SanPhamTheoLoaiController(Services service)
        {
            this._service = service;
        }
        [Area("Admin"), HttpGet]
        public IActionResult Index()
        {
            List<LoaiSanPham> lvt = _service.danhSachLoaiSP().ToList();
            return View(lvt);
        }

        [Area("Admin"), HttpGet]
        public IActionResult ListSanPham(string Id)
        {
            var sanPham = _service.danhSachSanPham(Id).ToList();
            sanPham.ToList();
            ViewBag.tenLoai = _service.getLoai(Id).TenLoaiSp;
            return View(sanPham);
        }
        [Area("Admin"), HttpGet]
        public IActionResult ListAllSanPham()
        {
            List<SanPham> sp = _service.danhSachSanPham().ToList();
            return View(sp);
        }
        [Area("Admin"), HttpGet]
        public IActionResult DetailSP(String Id)
        {
            SanPham sp = _service.getSanPham(Id);
            ViewBag.tenLoai = sp.MaLoaiSpNavigation.TenLoaiSp;
            return View(sp);
        }
    }
}
