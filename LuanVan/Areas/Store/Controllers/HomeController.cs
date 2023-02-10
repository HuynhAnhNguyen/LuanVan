using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly Services _service;

        public HomeController(Services service)
        {
            this._service = service;
        }

        [Area("Store")]
        public IActionResult Index()
        {
            var maKH = HttpContext.Session.GetString("CurrentUserID");
            ViewBag.Loai = _service.danhSachLoaiSP().ToList();
            ViewData["path"] = "/images/product/";
            if (maKH != null)
            {
                ViewData["cart_items"] = _service.danhSachGioHang(0, maKH).ToList();
            }
            else
            {
                ViewData["cart_items"] = new List<GioHang>();
            }

            ViewData["hot-items"] = _service.danhSachSanPham().ToList();
            return View(_service.danhSachSanPham().ToList());
        }

        [Area("Store"), HttpGet]
        public IActionResult getListCartItem()
        {
            return View(_service.danhSachGioHang().ToList());
        }
    }
}
