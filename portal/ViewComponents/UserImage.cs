using Microsoft.AspNetCore.Mvc;

namespace portal.ViewComponents
{
    public class UserImage : ViewComponent
    {
        private readonly IHttpContextAccessor _http;
        public UserImage(IHttpContextAccessor http)
        {
            _http = http;
        }

        public IViewComponentResult Invoke()
        {
            var image = _http.HttpContext.Session.GetString("image");
            ViewBag.image = image;
            return View();
        } 
    }
}
