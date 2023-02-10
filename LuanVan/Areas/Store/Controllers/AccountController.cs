using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Store.Controllers
{
    public class AccountController : Controller
    {
		private readonly Services _service;

		public AccountController(Services service)
		{
			this._service = service;
		}

		[Area("Store"), HttpGet]
		public IActionResult Info()
		{
			if (string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUserID")))
			{
				return RedirectToAction("Index", "Home");
			}
			var model = _service.getKH(HttpContext.Session.GetString("CurrentUserID"));
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
			return View(model);
		}
	}
}
