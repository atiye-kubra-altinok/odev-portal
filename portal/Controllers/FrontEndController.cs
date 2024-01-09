using Microsoft.AspNetCore.Mvc;
using portal.Models;

namespace portal.Controllers
{
    public class FrontEndController : Controller
    {
        private readonly Context _c;

        public FrontEndController(Context c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            var values = _c.derslers.ToList();
            return View(values);
        }
    }
}
