namespace WebAPI.Data
{
    using Microsoft.AspNetCore.Identity;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options, bool isInProduction)
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;

            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 15;
            options.Lockout.DefaultLockoutTimeSpan = new System.TimeSpan(hours: 1, 0, 0);
        }
    }
}
