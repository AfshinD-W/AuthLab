using Microsoft.AspNetCore.Identity;

namespace AuthLab.Infrastructure.Identity.Validators
{
    public class PasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : IdentityUser
    {
        private readonly HashSet<string> BlackList = ["1qaz!QAZ", "password"];

        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string? password)
        {

            List<IdentityError> errors = [];

            if (string.IsNullOrWhiteSpace(password))
            {
                return Task.FromResult(
                    IdentityResult.Failed(new IdentityError
                    {
                        Code = "Password.Required",
                        Description = "Password is required."
                    }));
            }

            if (BlackList.Contains(password))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password.Blacklisted",
                    Description = "This password is blacklisted and cannot be used."
                });
            }

            if (!string.IsNullOrWhiteSpace(user.UserName) &&
                password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
            {
                errors.Add(new IdentityError
                {
                    Code = "Password.ContainsUserName",
                    Description = "Your password cannot contain your username."
                });
            }

            return Task.FromResult(
                errors.Count == 0
                    ? IdentityResult.Success
                    : IdentityResult.Failed([.. errors]));
        }
    }
}
