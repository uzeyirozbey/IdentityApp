using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityBlogApp.Models.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Required(ErrorMessage = "Mail alanı boş bırakılamaz!!")]
        [Display(Name = "Email : ")]
        public string Email { get; set; }
    }
}
