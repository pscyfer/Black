using Microsoft.AspNetCore.Mvc;

namespace Administrator.ViewComponents
{
    public class FooterComponent : ViewComponent
    {
        public FooterComponent()
        {

        }
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
