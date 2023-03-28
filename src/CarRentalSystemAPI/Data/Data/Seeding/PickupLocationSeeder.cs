namespace WebAPI.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using WebAPI.Data.Models;

    public class PickupLocationSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();

            if (!dbContext.PickupLocations.Any())
            {
                dbContext.Add(new PickupLocation
                {
                    LocationName = configuration["InitialLocation"],
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
