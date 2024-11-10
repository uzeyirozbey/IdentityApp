using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityBlogApp.Models;
using NetCoreIdentityBlogApp.Extensions;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCoreIdentityBlogApp.Models.ViewModels;

namespace NetCoreIdentityBlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _Logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _SignInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _Logger = logger;
            _UserManager = userManager;
            _SignInManager = signInManager;
        }

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        public IActionResult SignIn() => View();


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _UserManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
                return View();
            }

            var SignInResult = await _SignInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

            if (SignInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }


            if (SignInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriş yapamazsınız." });
            }

            //Yanlış girişlerde sistemi geçici süreliğine kitlendiğine dair bilgi verme.
            ModelState.AddModelErrorList(new List<string>() { $"Email veya Şifre Yanlış.(Başarısız giriş sayısı= {await _UserManager.GetAccessFailedCountAsync(hasUser)})" });
            return View();
        }

        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid) return View();

            var identityResult = await _UserManager.CreateAsync(new()
            {
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Email = request.Email
            }, request.Password);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir";
                return RedirectToAction(nameof(HomeController.SignUp));
            }

            //Burda Extension kullandık
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            //link.https://localhost:7009
            var hasUser = await _UserManager.FindByEmailAsync(request.Email);
            if (hasUser == null) {
                ModelState.AddModelError(string.Empty, "Bu e-posta adresine ait kullanici bulunamamıştur");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
