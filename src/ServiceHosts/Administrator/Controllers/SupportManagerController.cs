using Microsoft.AspNetCore.Mvc;

namespace Administrator.Controllers
{
    public class SupportManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
