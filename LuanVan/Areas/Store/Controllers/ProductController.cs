using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Store.Controllers
{
    public class ProductController : Controller
    {
		private readonly Services _service;

		public ProductController(Services service)
		{
			this._service = service;
		}

		[Area("Store"), HttpGet]
		public IActionResult Search(string key)
		{
			if (key != "")
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
				ViewData["hot_items"] = _service.danhSachSanPham().ToList();
				ViewData["key"] = key;
				return View(_service.timKiem(key).ToList());
			}
			return View();
		}


		[Area("Store"), HttpGet]
		public IActionResult List_product(string maLoai)
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
			ViewData["hot_items"] = _service.danhSachSanPham().ToList();
			ViewData["loaivt"] = _service.getLoai(maLoai)?.TenLoaiSp;
			return View(_service.danhSachSanPham(maLoai));
		}

		[Area("Store"), HttpGet]
		public IActionResult Detail(string maSP)
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
			ViewData["hot_items"] = _service.danhSachSanPham().ToList();
			return View(_service.GetSanPham(maSP));
		}

		[Area("Store"), HttpPost]
		public PartialViewResult Get_by_cate(string maloai)
		{
			ViewData["path"] = "/images/product/";
			var model = _service.danhSachSanPham(maloai).ToList().Take(8);
			if (maloai == null)
			{
				model = _service.danhSachSanPham().ToList().Take(12);
			}
			return PartialView("_Product_Fillter", model);
		}
	}
}
