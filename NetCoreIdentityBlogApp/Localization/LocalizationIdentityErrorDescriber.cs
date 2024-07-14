using Microsoft.AspNetCore.Identity;

namespace NetCoreIdentityBlogApp.Localization
{
    public class LocalizationIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DublicateUserName", Description = $"Bu {userName}  daha önce başka bir kullanıcı tarafından alınmıştır"};
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DublicateMail",  Description = $"Bu {email}  daha önce başka bir kullanıcı tarafından alınmıştır"};
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = "Şifre en az 6 karakterli olmalıdır" };
        }

    }
}
