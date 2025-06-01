using Microsoft.AspNetCore.Mvc;

namespace ChineseAnimeCatalog.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
