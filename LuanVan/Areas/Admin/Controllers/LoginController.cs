using LuanVan.Areas.Admin.Models;
using LuanVan.Models;
using Microsoft.AspNetCore.Mvc;

namespace LuanVan.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly Services _service;
        public LoginController(Services service)
        {
            _service = service;
        }
        [Area("Admin"), HttpGet]
        public ActionResult Index()
        {
            var session = HttpContext.Session;
            string adminUser = session.GetString("CurrentStaff");
            if (adminUser != null)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginModel model = new LoginModel();
            if (HttpContext.Request.Cookies.ContainsKey("phonenumber"))
            {
                model.NumberPhone = HttpContext.Request.Cookies["phonenumber"];
            }
            return View(model);
        }

        [Area("Admin"), HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                NhanVien search = _service.loginNV(model.NumberPhone, model.Password);
                if (search == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                    return View(model);
                }
                if (model.Rememberme)
                {
                    CookieOptions opt = new CookieOptions();
                    opt.Expires = DateTime.Now.AddDays(3);
                    HttpContext.Response.Cookies.Append("phonenumber", model.NumberPhone, opt);
                }
                else
                {
                    if (HttpContext.Request.Cookies.ContainsKey("phonenumber"))
                    {
                        CookieOptions opt = new CookieOptions();
                        opt.Expires = DateTime.Now.AddDays(-1);
                        HttpContext.Response.Cookies.Append("phonenumber", model.NumberPhone, opt);
                    }
                }
                var session = HttpContext.Session;
                session.SetString("CurrentStaff", search.HoNhanVien + " " + search.TenNhanVien);
                session.SetString("CurrentStaffID", search.MaNhanVien.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [Area("Admin"), HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
