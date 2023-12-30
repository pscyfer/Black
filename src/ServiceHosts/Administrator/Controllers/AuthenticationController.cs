using Identity.Core.Dto.User;
using Identity.Core.Services;
using Identity.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.Controllers
{
    [Authorize]
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model, string redirect)
        {
            ModelState.Remove("redirect");
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.SignInWithPassword(new
                SignInWithPasswordQueryDto(model.PhoneNumber,
                model.Password, model.IsRemember, true));
            if (result.Data == null) ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");
            else
            {
                if (!result.Data.Succeeded && !result.Data.IsNotAllowed && !result.Data.IsLockedOut && !result.Data.RequiresTwoFactor)
                    ModelState.AddModelError(string.Empty, "کاربری با این مشخصات یافت نشد");

                if (result.Data.Succeeded) return Redirect(!string.IsNullOrWhiteSpace(redirect)?redirect:"/");
                if (result.Data.IsNotAllowed) ModelState.AddModelError(string.Empty, "حساب کاربری شما فعال نیست");
                if (result.Data.IsLockedOut) ModelState.AddModelError(string.Empty, "حساب کاربری شما قفل شده است.");
                if (result.Data.RequiresTwoFactor) ModelState.AddModelError(string.Empty, "نیاز به احراز هویت 2 مرحله ای می باشد.");
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _userService.RegisterUser(new RegisterUserCommandDto(model.PhoneNumber, model.Password));
            if (result.IsSuccessed) return RedirectToAction(nameof(Login), "Authentication");

            ModelState.AddModelError(string.Empty, result.Message);
            return View(model);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOut();
            return Redirect("/");
        }
    }
}
