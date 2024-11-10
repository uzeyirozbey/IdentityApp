using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCoreIdentityBlogApp.Models;
using NetCoreIdentityBlogApp.Models.ViewModels;
using NetCoreIdentityBlogApp.Extensions;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreIdentityBlogApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public IActionResult Index()
        {
            return View();
        }

        public MemberController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public  async Task Logout()
        {
           await _signInManager.SignOutAsync();
            //return RedirectToAction("Index","Home");
        }
    }
}
