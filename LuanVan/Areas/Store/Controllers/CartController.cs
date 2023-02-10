using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Store.Controllers
{
    public class CartController : Controller
    {
		private readonly Services _service;
		public CartController(Services service)
		{
			this._service = service;
		}

		[Area("Store"), HttpGet]
		public IActionResult MyItems()
		{
			var maKH = HttpContext.Session.GetString("CurrentUserID");
			if (maKH == null)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Loai = _service.danhSachLoaiSP().ToList();
				ViewData["path"] = "/images/product/";
				ViewData["cart_items"] = _service.danhSachGioHang(0, maKH).ToList();
				return View(_service.danhSachGioHang(0, maKH).ToList());
			}
		}

		[Area("Store"), HttpPost]
		public PartialViewResult RemoveItems(string maGH)
		{
			var model = _service.getGioHang(maGH);
			if (model.TrangThai == 0)
			{
				_service.xoaGioHang(maGH);
			}
			return get_cart();

		}

		[Area("Store"), HttpPost]
		public PartialViewResult RemoveCartItem(string maGH)
		{
			var maKH = HttpContext.Session.GetString("CurrentUserID");
			var model = _service.getGioHang(maGH);
			if (model.TrangThai == 0)
			{
				_service.xoaGioHang(maGH);
			}

			ViewData["path"] = "/images/product/";
			return PartialView("_Cart_Full", _service.danhSachGioHang(0, maKH).ToList());
		}

		[Area("Store"), HttpPost]
		public JsonResult AddItems(string masp)
		{
			var maKH = HttpContext.Session.GetString("CurrentUserID");
			if (maKH == null)
			{
				return Json(null);
			}
			string kq = "Not add";
			if (!string.IsNullOrEmpty(masp))
			{
				kq = _service.themGioHang(masp, maKH);
			}
			return Json(kq);
		}

		[Area("Store"), HttpPost]
		public PartialViewResult get_cart()
		{
			var maKH = HttpContext.Session.GetString("CurrentUserID");
			return PartialView("_Cart", _service.danhSachGioHang(0, maKH).ToList());
		}

		[Area("Store"), HttpPost]
		public JsonResult Increase(string maDD, int soLuong)
		{
			_service.increase(maDD, soLuong);
			return Json(maDD + " " + soLuong);
		}
	}
}
