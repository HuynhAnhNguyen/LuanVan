using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Store.Controllers
{
    public class ReceiptController : Controller
    {
		private readonly Services _service;
		public ReceiptController(Services service)
		{
			this._service = service;
		}
		[Area("Store"), HttpGet]
		public IActionResult List()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
			{
				return RedirectToAction("Index", "Home");
			}

			var model = _service.danhSachHoaDon(HttpContext.Session.GetString("CurrentUserID")).ToList();

			var makh = HttpContext.Session.GetString("CurrentUserID");
			ViewBag.Loai = _service.danhSachLoaiSP().ToList();
			ViewData["path"] = "/images/product/";
			if (makh != null)
			{
				ViewData["cart_items"] = _service.danhSachGioHang(0, makh).ToList();
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
			}
			return View(model);
		}

		[Area("Store"), HttpGet]
		public IActionResult Detail(string mahd)
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
			{
				return RedirectToAction("Index", "Home");
			}
			var model = _service.getHoaDon(mahd);

			var makh = HttpContext.Session.GetString("CurrentUserID");
			ViewBag.Loai = _service.danhSachLoaiSP().ToList();
			ViewData["path"] = "/images/product/";
			ViewData["hoadon"] = _service.getHoaDon(mahd);
			ViewData["sum"] = _service.tongGiaTri(mahd);
			if (makh != null)
			{
				ViewData["cart_items"] = _service.danhSachGioHang(0, makh).ToList();
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
			}
			return View(model);
		}
	}
}
