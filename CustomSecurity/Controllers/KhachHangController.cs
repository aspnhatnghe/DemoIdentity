using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomSecurity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomSecurity.Controllers
{
    [Authorize]
    public class KhachHangController : Controller
    {
        private readonly MyeStoreContext ctx;
        public KhachHangController(MyeStoreContext db)
        {
            ctx = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                //kiểm tra username/pass có trong database
                //KhachHang kh = ctx.KhachHang.SingleOrDefault(p => p.MaKh == model.Username && p.MatKhau == model.Password);
                KhachHang kh = ctx.KhachHang.SingleOrDefault(p => p.MaKh == model.Username);                

                if (kh != null)//thành công
                {
                    string matkhauHash = (model.Password + kh.RandomKey).ToMD5();
                    if(kh.MatKhau != matkhauHash)
                    {
                        ModelState.AddModelError("loi", "Sai mật khẩu"); 
                        return View();
                    }
                    //khai báo các thông tin Identity
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, kh.HoTen),
                        new Claim(ClaimTypes.Email, kh.Email)
                    };
                    // create identity
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);

                    //nếu có trang yêu cầu trước đó
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Profile", "KhachHang");//default
                    }
                }

            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Register(KhachHang kh)
        {
            Random rd = new Random();
            string randomKey = rd.Next(1000, 10000).ToString();

            kh.RandomKey = randomKey;
            kh.MatKhau = (kh.MatKhau + randomKey).ToMD5();

            ctx.Add(kh);
            ctx.SaveChanges();
            return View();
        }
    }
}