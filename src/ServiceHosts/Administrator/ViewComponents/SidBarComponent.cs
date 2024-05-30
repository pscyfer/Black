using Microsoft.AspNetCore.Mvc;

namespace Administrator.ViewComponents
{
    public class SidBarComponent : ViewComponent
    {
        public SidBarComponent()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
