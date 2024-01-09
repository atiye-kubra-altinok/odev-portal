using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NETCore.Encrypt.Extensions;
using portal.Models;
using portal.ViewModel;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;

namespace portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly Context _c;
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;
        private readonly RoleManager<Rol> _role;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, Context c, UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager, RoleManager<Rol> role)
        {
            _logger = logger;
            _config = config;
            _c = c;
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Cookie bazlı oturum açma
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(LoginModel model)
        //{
        //    var hashedpass = MD5Hash(model.Sifre);
        //    var user = _c.users.Where(s => s.KullaniciAdi == model.KullaniciAdi && s.Sifre == hashedpass).SingleOrDefault();

        //    if (user == null)
        //    {
        //        ViewBag.message = "Kullanıcı adı veya parola yanlış";             
        //    }

        //    List<Claim> claims = new List<Claim>() {

        //        new Claim(ClaimTypes.Name, user.Adi),
        //        new Claim(ClaimTypes.Surname, user.Soyadi),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Role,user.Rol),
        //        new Claim("KullaniciAdi",user.KullaniciAdi),
        //    };
        //    ClaimsIdentity idetity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    ClaimsPrincipal principal = new ClaimsPrincipal(idetity);

        //    AuthenticationProperties properties = new AuthenticationProperties()
        //    {
        //        AllowRefresh = true,              
        //    };

        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        //    HttpContext.Session.SetInt32("userId" , user.Id);
        //    HttpContext.Session.SetString("image", user.Gorsel);
        //    if(user.Rol == "Ogretmen")
        //    {
        //        return Redirect("/Ogrenciler/Index/");
        //    }
        //    else
        //    {
        //        return Redirect("/Dersler/Derslerim");
        //    }

        //} 
        #endregion


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.email);
                if (user == null)
                {
                    ViewBag.message = "Böyle bir kullanıcı bulunamadı";
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Sifre, false, false);

                    if (result.Succeeded)
                    {

                        var userRoles = await _userManager.GetRolesAsync(user);

                        if (userRoles.FirstOrDefault() == "Admin")
                        {
                            HttpContext.Session.SetInt32("userId", user.Id);

                            return Redirect("/Uye/Index/");
                        }
                        else if (userRoles.FirstOrDefault() == "Ogretmen"){
                            HttpContext.Session.SetInt32("userId", user.Id);
                            return Redirect("/Ogrenciler/Index/");
                        }
                        else
                        {
                            HttpContext.Session.SetInt32("userId", user.Id);
                            return Redirect("/Dersler/Derslerim");
                        }

                        //if (User.IsInRole("Ogretmen"))
                        //{
                        //    HttpContext.Session.SetInt32("userId", user.Id);

                        //    return Redirect("/Ogrenciler/Index/");
                        //}
                        //else if (User.IsInRole("Admin"))
                        //{
                        //    HttpContext.Session.SetInt32("userId", user.Id);

                        //    return Redirect("/Uye/Index/");
                        //}
                        //else
                        //{
                        //    HttpContext.Session.SetInt32("userId", user.Id);
                        //    return Redirect("/Dersler/Derslerim");
                        //}

                    }
                }
            }
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Kullanici
                {
                    Adi = model.Adi,
                    Soyadi = model.Soyadi,
                    UserName = model.KullaniciAdi,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Sifre);
                if (result.Succeeded)
                {

                    var userRole = await _userManager.FindByEmailAsync(model.Email);

                    await _userManager.AddToRoleAsync(userRole, "Ogrenci");

                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        #region Kayit Olma
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Register(RegisterModel model)
        //{
        //    if (_c.users.Count(s => s.KullaniciAdi == model.KullaniciAdi) > 0)
        //    {
        //        ViewBag.message("Girilen Kullanıcı Adı Kayıtlıdır!");

        //    }
        //    if (_c.users.Count(s => s.Email == model.Email) > 0)
        //    {
        //        ViewBag.message("Girilen E-Posta Adresi Kayıtlıdır!");

        //    }



        //    var hashedpass = MD5Hash(model.Sifre);
        //    var user = new AppUser();

        //    if (model.Gorsel != null)
        //    {
        //        var extension = Path.GetExtension(model.Gorsel.FileName);
        //        var newimagename = Guid.NewGuid() + extension;
        //        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Gorsel/", newimagename);
        //        var stream = new FileStream(location, FileMode.Create);
        //        model.Gorsel.CopyTo(stream);
        //        user.Gorsel = "/Gorsel/" + newimagename;
        //    }


        //    user.Adi = model.Adi;
        //    user.Soyadi = model.Soyadi;
        //    user.KullaniciAdi = model.KullaniciAdi;
        //    user.Sifre = hashedpass;
        //    user.Email = model.Email;            
        //    user.Rol = "Ogrenci";
        //    _c.users.Add(user);
        //    _c.SaveChanges();

        //    Ogrenci ogrenci = new Ogrenci();
        //    ogrenci.userId = user.Id;
        //    _c.Add(ogrenci);
        //    _c.SaveChanges();                      

        //    return RedirectToAction("Login");
        //} 
        #endregion



        //public string MD5Hash(string pass)
        //{
        //    var salt = _config.GetValue<string>("AppSettings:MD5Salt");
        //    var password = pass + salt;
        //    var hashed = password.MD5();
        //    return hashed;
        //}

    }
}
