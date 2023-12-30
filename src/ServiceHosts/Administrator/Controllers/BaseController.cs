using Identity.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.Controllers
{

    public class BaseController : Controller
    {
        
        
       

        public Guid GetCurrnetUserId
        {
            get
            {
                if (AuthHelper.IsUserAuthenticated(User))
                    return Guid.Parse(AuthHelper.GetUserId(User));
                throw new Exception("User Not  Authenticated .");
            }
        }

        #region MenuActiovations
        [NonAction]
        protected void ActiveMenu(string menuId)
        {
            var menu = new
            {
                menuId = menuId,
            };
            TempData["ActiveMenu"] = System.Text.Json.JsonSerializer.Serialize(menu);
        }
        public IActionResult SwalNotificationActionResult()
        {
            return Content(TempData["notification"]?.ToString() ?? throw new InvalidOperationException());
        }
        [NonAction]
        protected void SetAjaxNotification(string notification)
        {
            TempData["notification"] = notification;
        }

        #endregion
    }
}
