using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using portal.Models;

namespace portal.Controllers
{
    [Authorize]
    public class OdevController : Controller
    {
        private readonly Context _c;

        public OdevController(Context c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            var values = _c.odevs.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Odev Odev)
        {
            Odev Odevler = new Odev();
            Odevler.OdevKonusu = Odev.OdevKonusu;
            _c.odevs.Add(Odevler);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            var values = _c.odevs.Find(id);
            _c.odevs.Remove(values);
            _c.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            var values = _c.odevs.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Duzenle(Odev Odev, int id)
        {
            var values = _c.odevs.Find(id);
            values.OdevKonusu = Odev.OdevKonusu;
            _c.odevs.Update(values);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
