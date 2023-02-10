using LuanVan.Areas.Store.Models;
using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System;
using static System.Net.WebRequestMethods;
using NToastNotify;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace LuanVan.Areas.Store.Controllers
{
    public class LoginController : Controller
    {
        private readonly Services _service;
        private readonly INotyfService _notyf;
		private readonly IToastNotification _toastNotification;
		public LoginController(Services service,  INotyfService _notyf, IToastNotification _toastNotification)
		{
			this._service = service;
			this._toastNotification= _toastNotification;
            this._notyf = _notyf;
		}

		[Area("Store"), HttpGet]
		public ActionResult Index()
		{
			var session = HttpContext.Session;
			string user = session.GetString("CurrentUser");
			if (user != null)
			{
				return RedirectToAction("Index", "Home");
			}

			LoginModel model = new LoginModel();
			if (HttpContext.Request.Cookies.ContainsKey("sdt"))
			{
				model.Username = HttpContext.Request.Cookies["sdt"];
			}
			return View(model);
		}

		[Area("Store"), HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				KhachHang search = _service.loginKH(model.Username, model.Password);
				if (search == null)
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
					return View(model);
				}
				if (model.Rememberme)
				{
					CookieOptions opt = new CookieOptions();
					opt.Expires = DateTime.Now.AddDays(3);
					HttpContext.Response.Cookies.Append("sdt", model.Username, opt);
				}
				else
				{
					if (HttpContext.Request.Cookies.ContainsKey("sdt"))
					{
						CookieOptions opt = new CookieOptions();
						opt.Expires = DateTime.Now.AddDays(-1);
						HttpContext.Response.Cookies.Append("sdt", model.Username, opt);
					}
				}
				var session = HttpContext.Session;
				session.SetString("CurrentUser", search.HoKhachHang + " " + search.TenKhachHang);
				session.SetString("CurrentUserID", search.MaKhachHang.ToString());
				Console.WriteLine(search.MaKhachHang.ToString());

                _notyf.Success("Đăng nhập thành công!", 3);
                //_notyf.Success("Success Notification that closes in 10 Seconds.", 3);


                return RedirectToAction("Index", "Home");
			}
			return View(model);
		}

		[Area("Store"), HttpGet]
		public ActionResult Logout()
		{
			var session = HttpContext.Session;
			session.Clear();
			return RedirectToAction("Index", "Home");
		}

		[Area("Store"), HttpGet]
		public IActionResult Register()
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
			return View();
		}

        [Area("Store"), HttpPost]
		public IActionResult Register(RegisterModel model)
		{
            string otp = _service.OTP();
            if (ModelState.IsValid)
			{
                if (model.MatKhau == model.MatKhau2)
				{
					
					Console.WriteLine("otp send mail: " + otp);

					List<string> email= _service.GetEmailListFromDB();
					List<string> sdt = _service.GetSdtListFromDB();

                    foreach (var e in email){
						if(e == model.Email)
						{
                            //ModelState.AddModelError("", "Địa chỉ email đã tồn tại");
							_toastNotification.AddErrorToastMessage("Địa chỉ email đã tồn tại");
							return View(model);
                        }
                        
                    }

                    foreach (var s in sdt)
                    {
                        if (s == model.SoDienThoai)
                        {
                            //ModelState.AddModelError("", "Số điện thoại đã tồn tại");
                            _toastNotification.AddErrorToastMessage("Số điện thoại đã tồn tại");
                            return View(model);
                        }

                    }


                    string maKH=_service.themKH(model);

					//_service.suaTTKhachHang(maKH, otp); // sửa lại trạng thái KH= otp

					//_toastNotification.AddSuccessToastMessage("Đăng ký tài khoản thành công!");
					//_toastNotification.AddAlertToastMessage("Hi");

					//// Info Toast
					_toastNotification.AddInfoToastMessage("Đăng ký tài khoản thành công!");

					//// Error Toast
					//_toastNotification.AddErrorToastMessage("Woops an error occured.");

					//// Warning Toast
					//_toastNotification.AddWarningToastMessage("Here is a simple warning!");


					return RedirectToAction("Index", "Login");
				}
				else
				{
					ModelState.AddModelError("", "Xác nhận mật khẩu không trùng khớp");
					return View(model);
				}
				
			}

            return View(model);
		}

		[Area("Store"), HttpGet]
		public IActionResult CheckOTP()
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
			return View();
		}

		[Area("Store"), HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CheckOTP(OTPModel model)
		{
			//Console.WriteLine(maKH);
			//string getOTP = _service.getTrangThai(maKH);

			//Console.WriteLine("getOTP: " + getOTP);

			Console.WriteLine("Model OTP: " + model.OTP);
			if (ModelState.IsValid)
			{
				if (model.OTP == "")
				{


					//_service.themKH(registerModel);
					return RedirectToAction("Index", "Login");
				}
				else
				{
					ModelState.AddModelError("", "Mã OTP không hợp lệ!");
					return View(model);
				}
			}
			return View(model);
		}

        [Area("Store"), HttpPost]
        public JsonResult CheckEmail(string email)
        {
			// Lấy danh sách email từ database
            var emailList = _service.GetEmailListFromDB();

            // Kiểm tra xem email có tồn tại trong danh sách hay không
            var emailExists = emailList.Any(e => e.Equals(email, StringComparison.OrdinalIgnoreCase));

            // Trả về kết quả
            return Json(new { emailExists });
        }



    }
}
