using Microsoft.AspNetCore.Identity;
using NetCoreIdentityBlogApp.Models;

namespace NetCoreIdentityBlogApp.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors      = new List<IdentityError>();
            var IsDigit     = int.TryParse(user.UserName[0]!.ToString(),out _);
            if (IsDigit)
            {
                errors.Add(new() {  Code ="UserNameContainsFirstLetterDigit", Description="Kullanıcı adının ilkı karakteri sayısal bir karakter içermez."});
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);

        }
    }
}
