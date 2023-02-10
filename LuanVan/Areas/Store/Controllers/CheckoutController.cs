using LuanVan.Areas.Store.Models;
using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.Json;

namespace LuanVan.Areas.Store.Controllers
{
    public class CheckoutController : Controller
    {
		private readonly Services _service;

		public CheckoutController(Services service)
		{
			this._service = service;
		}

		[Area("Store"), HttpGet]
		public IActionResult Index()
		{
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
			return View(_service.danhSachGioHang(2).ToList().Take(2));
		}


		[Area("Store"), HttpGet]
		public IActionResult Confirm(string listdd) 
		{
			var st = JsonSerializer.Deserialize<List<string>>(listdd); 
			var list = new List<GioHang>();

			if (st?.Count() > 0 && st.First().Contains("DD"))
			{
				foreach (var s in st)
				{
					var dd = _service.getGioHang(s);
					list.Add(dd);
				}
			}

			if (st?.Count() > 0 && !st.First().Contains("DD"))
			{
				string madd = _service.themGioHang(st.First());
				list.Add(_service.getGioHang(madd));
			}

			var makh = HttpContext.Session.GetString("CurrentUserID");

			ViewBag.Loai = _service.danhSachLoaiSP().ToList();
			ViewData["path"] = "/images/product/";
			if (makh != null)
			{
				ViewData["cart_items"] = _service.danhSachGioHang(0, makh).ToList();
				HoaDonModel info = new HoaDonModel();
				var kh = _service.getKH(makh);
				info.Holot = kh.HoKhachHang;
				info.Ten = kh.TenKhachHang;
				info.SoDienThoai = kh.SoDienThoai;
				ViewData["info"] = info;
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
				ViewData["info"] = null;
			}
			return View(list);
		}

		[Area("Store"), HttpPost]
		public IActionResult Receipt(HoaDonModel model)
		{
			var st = JsonSerializer.Deserialize<List<string>>(model.GioHangs);
			var list = new List<GioHang>();

			if (st?.Count() > 0 && st.First().Contains("DD"))
			{
				foreach (var s in st)
				{
					var dd = _service.getGioHang(s);
					if (dd.TrangThai != 1)
						list.Add(dd);
				}
			}

			if (st?.Count() > 0 && !st.First().Contains("GH"))
			{
				string madd = _service.themGioHang(st.First());
				list.Add(_service.getGioHang(madd));
			}


			if (list.Count == 0)
			{
				return RedirectToAction("Index", "Home");
			}

			var makh = HttpContext.Session.GetString("CurrentUserID");
			ViewBag.Loai = _service.danhSachLoaiSP().ToList();
			ViewData["path"] = "/images/product/";
			string new_mahd;
			if (makh != null)
			{
				ViewData["cart_items"] = _service.danhSachGioHang(0, makh).ToList();
				new_mahd = _service.themHoaDon(makh);
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
				new_mahd = _service.themHoaDon();
			}
			foreach (var dd in list)
			{
				_service.themChiTietHD(new_mahd, dd.MaGioHang);
			}
			if (makh != null)
			{
				ViewData["cart_items"] = _service.danhSachGioHang(0, makh).ToList();
			}
			else
			{
				ViewData["cart_items"] = new List<GioHang>();
			}
			model.MaHoaDon = new_mahd;
			ViewData["info"] = model;


            return View(list);
		}
	}
}
