using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Validators
{
    public class PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : IdentityUser
    {
        private readonly ICollection<string> _blackList = ["1qaz!QAZ", "password"];

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {
            if (!string.IsNullOrEmpty(password) && _blackList.Any(c => string.Equals(c, password, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "PassInBlackList",
                    Description = "this password is in black list you can't use it."
                }));
            }

            if (!string.IsNullOrEmpty(password) && password.Contains(user.UserName!, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNameInPass",
                    Description = "You can't use your username in your password."
                }));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
