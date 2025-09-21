using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_353504_Antonava_Lab3.Models;

namespace WEB_353504_Antonava_Lab3.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            ViewData["TitleText"] = "Лабораторная работа №3ы";

            var items = new List<ListDemo>
            {
                new ListDemo { Id = 1, Name = "Item 1" },
                new ListDemo { Id = 2, Name = "Item 2" },
                new ListDemo { Id = 3, Name = "Item 3" }
            };

            ViewBag.SelectList = new SelectList(items, "Id", "Name");

            return View();
        }
    }
}
