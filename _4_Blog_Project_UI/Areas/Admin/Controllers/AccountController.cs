using Blog_Project_Application.Services.AppUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.AppUserDTO;

namespace _4_Blog_Project_UI.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserServices _appUserServices;

        //[Authorize]
        public AccountController(IAppUserServices appUserServices)
        {
            _appUserServices = appUserServices;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "");
            }
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserServices.Register(registerDTO);
                if (result.Succeeded)
                    return RedirectToAction("Index", "");

                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Bazı şeyler yanlış gitti";
                }
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                // return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                return RedirectToAction("Index", "");
            }
            
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserServices.Login(model);
                if (result.Succeeded)
                    return RedirectToLocal(ReturnUrl);

                ModelState.AddModelError("", "Invalid Login Attempt");
                //foreach (var item in result.Errors)
                //{

                //    ModelState.AddModelError(string.Empty, item.Description);
                //    TempData["Error"] = "Bazı şeyler yanlış gitti";
                //}
            }
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
            {
                return RedirectToAction("Index", "");
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await _appUserServices.LogOut();
            return RedirectToAction("Index", "Home");//home controllerdaki index'e git
        }
        public async Task<IActionResult> Edit(string username)
        {
            if (username != "")
            {
                UpdateProfileDTO user = await _appUserServices.GetByUserName(username);
                return View(user);
            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            //if (model.ImagePath == null)
            //    model.ImagePath = $"/images/defaultuser.jpg";
            if (ModelState.IsValid)
            {
                
                try
                {
                    await _appUserServices.UpdateUser(model);
                }
               catch (Exception)
                {
                    TempData["Error"] = "Something went wrong";
                }
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Your profile hasn't been updated!";
            return View();
        }

        [HttpGet]
        public IActionResult ProfileDetails()
        {
          
                return RedirectToAction("Index");
            
        }
        //[HttpPost]
        //public async Task<IActionResult> ProfileDetails()
        //{

        //}
    }
}
