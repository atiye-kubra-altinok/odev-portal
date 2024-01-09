using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal.Models;
using portal.ViewModel;

namespace portal.Controllers
{
    public class UyeController : Controller
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly RoleManager<Rol> _rol;

        public UyeController(UserManager<Kullanici> userManager, RoleManager<Rol> rol)
        {
            _userManager = userManager;
            _rol = rol;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _userManager.Users.Select(x => new AppUserModel
            {
                Id = x.Id,
                Adi = x.Adi,
                Soyadi = x.Soyadi,
                Email = x.Email,                
            }).ToListAsync();
            return View(values);
        }

        public async Task<IActionResult> Sil(int id)
        {
            var values = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(values);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Yetkilendirme(int id)
        {
            var user = _userManager.Users.FirstOrDefault
                 (x => x.Id == id);

            var roles = _rol.Roles.ToList();

            TempData["Userid"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<YetkilendirmeModel> yetkiModel = new List<YetkilendirmeModel>();

            foreach (var item in roles)
            {
                YetkilendirmeModel m = new YetkilendirmeModel();
                m.RoleID = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                yetkiModel.Add(m);
            }

            return View(yetkiModel);
        }

        [HttpPost]
        public async Task<IActionResult> Yetkilendirme(List<YetkilendirmeModel> model)
        {
            string username = HttpContext.Session.GetString("UserName");
            int userid = (int)TempData["Userid"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userid);
            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);                  

                    TempData["yetkimesaj"] = $"{user.UserName} isimli kullanıcıya {item.Name} yetkisi verildi.";
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);

                }

            }
            return Redirect("/Uye/Index/");
        }
    }
}
