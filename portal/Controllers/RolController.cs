using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using portal.Models;
using portal.ViewModel;

namespace portal.Controllers
{
    public class RolController : Controller
    {
        private readonly RoleManager<Rol> _role;
        private readonly Context _c;
        public RolController(RoleManager<Rol> role, Context c)
        {
            _role = role;
            _c = c;
        }

        public IActionResult Index()
        {
            var roles = _role.Roles.Select(x => new RolEkleModel()
            {
                Name = x.Name,
            }).ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult RolEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RolEkle(RolEkleModel model)
        {
            if (ModelState.IsValid)
            {
                Rol role = new Rol()
                {
                    Name = model.Name
                };

                var result = await _role.CreateAsync(role);
                if (result.Succeeded)
                {
                    _c.SaveChanges();
                    return RedirectToAction("RolEkle");
                }
            }
            return View(model);


        }
    }
}
