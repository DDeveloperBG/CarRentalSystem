namespace WebAPI.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using WebAPI.Common;
    using WebAPI.Data.Models;

    public class AdminSeeder : ISeeder
    {
        private readonly string adminUsernamePath = GlobalConstants.ConfigurationKeys.Admin.UsernameKey;
        private readonly string adminPasswordKeyPath = GlobalConstants.ConfigurationKeys.Admin.PasswordKey;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            var adminUsername = configuration[this.adminUsernamePath];
            if (!dbContext.Users.Any(user => user.UserName == adminUsername))
            {
                var user = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminUsername,
                    Forename = string.Empty,
                    Surname = string.Empty,
                    PersonalIdentificationNumber = string.Empty,
                };
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                await userManager.CreateAsync(user, configuration[this.adminPasswordKeyPath]);
                await userManager.AddToRoleAsync(user, GlobalConstants.Roles.AdministratorRoleName);

                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, code);
            }
        }
    }
}
