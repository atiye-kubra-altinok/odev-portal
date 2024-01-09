using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal.Models;
using portal.ViewModel;

namespace portal.Controllers
{
    public class DerslerController : Controller
    {
        private readonly Context _c;

        public DerslerController(Context c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            var values = _c.derslers.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Ders ders)
        {
            Ders dersler = new Ders();
            dersler.DersAdi = ders.DersAdi;
            _c.derslers.Add(dersler);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            var values = _c.derslers.Find(id);
            _c.derslers.Remove(values);
            _c.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Duzenle(int id)
        {
            var values = _c.derslers.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult Duzenle(Ders ders , int id)
        {
            var values = _c.derslers.Find(id);
            values.DersAdi = ders.DersAdi;
            _c.derslers.Update(values);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }

        //--------------------------------------------------
        //Ajax ile yapıldı
        public IActionResult Derslerim()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            var values = _c.ogrenciDersleri.Where(x => x.AppUserId == userId).Include(x=>x.Ders).Include(x=>x.Odev)
                .Select(x=> new OgrenciDersleriModel
                {
                    Id = x.Id,
                    DersAdi = x.Ders.DersAdi,
                    Odev = x.Odev.OdevKonusu,
                }).ToList();
            return View(values);

        }

        [HttpGet]
        public IActionResult YeniEkle()
        {           
            return View();
        }
        [HttpPost]
        public IActionResult YeniEkle(OgrenciDersleri ogrenciDersleri)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            OgrenciDersleri ogrenci = new OgrenciDersleri();
            ogrenci.DersId = ogrenciDersleri.DersId;
            ogrenci.AppUserId =Convert.ToInt32(userId);
            ogrenci.Durum = false;
            _c.ogrenciDersleri.Add(ogrenci);
            _c.SaveChanges();
            return RedirectToAction("Derslerim");
        }


        public IActionResult DersListele()
        {
            var values = _c.derslers.ToList();
            return Json(values);
        }


        public IActionResult DerslerimSil(int id)
        {
            var values = _c.ogrenciDersleri.Find(id);
            _c.ogrenciDersleri.Remove(values);
            _c.SaveChanges();
            return RedirectToAction("Derslerim");
        }
    }
}
