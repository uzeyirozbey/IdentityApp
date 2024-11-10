using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityBlogApp.Models.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel() { }
        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Display(Name = "Email : ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!!")]
        [Display(Name = "Şifre : ")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }

    }
}
