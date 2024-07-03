using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityBlogApp.Models;
using NetCoreIdentityBlogApp.ViewModels;
using System.Diagnostics;

namespace NetCoreIdentityBlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;


        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           var identityResult = await _userManager.CreateAsync(new()
            {
                UserName        = request.UserName,
                PhoneNumber     = request.Phone,
                Email           = request.Email
            },request.Password);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            //Hata varsa
            foreach (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
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
