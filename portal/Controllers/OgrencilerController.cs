using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using portal.Models;
using portal.ViewModel;

namespace portal.Controllers
{
    [Authorize]
    public class OgrencilerController : Controller
    {
        private readonly Context _c;

        public OgrencilerController(Context c)
        {
            _c = c;
        }

        public IActionResult Index()
        {


            var groupedValues = _c.ogrenciDersleri
    .Include(x => x.AppUser)
    .Include(x => x.Ders)
    .Include(x => x.Odev)
    .GroupBy(x => x.AppUserId) // AppUserId'ye göre gruplama
    .Select(group => new OgrenciDersleriModel
    {
        DersAdi = group.First().Ders.DersAdi,
        OgrenciAdiSoyadi = group.First().AppUser.Adi + " " + group.First().AppUser.Soyadi,
        Id = group.First().Id,
        UserId = group.Key,
        Odev = group.First().Odev.OdevKonusu,
        Durum = group.First().Durum,
    })
    .ToList();

            return View(groupedValues);




        }


        public IActionResult Detay(int id)
        {
            var values = _c.ogrenciDersleri.Where(x => x.AppUserId == id).Include(x => x.AppUser).Include(x => x.Ders).Include(x => x.Odev)
               .Select(x => new OgrenciDersleriModel
               {
                   DersAdi = x.Ders.DersAdi,
                   OgrenciAdiSoyadi = x.AppUser.Adi + " " + x.AppUser.Soyadi,
                   Id = x.Id,
                   Odev = x.Odev.OdevKonusu,
                   Durum = x.Durum,

               }).ToList();
            return View(values);
        }



        public IActionResult OdeviTamamla(int id)
        {
            var values = _c.ogrenciDersleri.Find(id);
            values.Durum = true;
            _c.ogrenciDersleri.Update(values);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult OdeviTamamlama(int id)
        {
            var values = _c.ogrenciDersleri.Find(id);
            values.Durum = false;
            _c.ogrenciDersleri.Update(values);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }


        //ajax ile listelendi view'de
        public IActionResult OdevListele()
        {
            var values = _c.odevs.ToList();
            return Json(values);
        }


        [HttpGet]
        public IActionResult Odev(int id)
        {
            var values = _c.ogrenciDersleri.Find(id);
            ViewBag.id = values.Id;
            return View(values);
        }

        [HttpPost]
        public IActionResult Odev(OgrenciDersleri ogrenciDersleri, int id)
        {
            var values = _c.ogrenciDersleri.Find(id);
            values.OdevId = ogrenciDersleri.OdevId;
            _c.ogrenciDersleri.Update(values);
            _c.SaveChanges();
            return RedirectToAction("Index");
        }






    }
}
