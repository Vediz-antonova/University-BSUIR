using Microsoft.AspNetCore.Mvc;

namespace WEB_353504_Antonava_Lab1.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
